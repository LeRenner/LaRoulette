using UnityEngine;
using Mirror;
using VivoxUnity;
using System.Linq;

namespace Player
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(NetworkTransform))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerBehavior : NetworkBehaviour
    {
        [Header("Camera")]
        public float sensX;
        public float sensY;

        float xRotation;
        float yRotation;

        [Header("Movement")]
        public float moveSpeed;
        public float dragForce;

        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        bool readyToJump = true;

        KeyCode jumpKey = KeyCode.Space;

        float hInput;
        float vInput;

        Vector3 moveDirection;
        Rigidbody rb;

        bool isGrounded;
        float height;

        private VivoxVoiceManager vivoxVoiceManager;
        private string LobbyChannelName = "default";

        void Awake(){
            vivoxVoiceManager = VivoxVoiceManager.Instance;
            vivoxVoiceManager.OnUserLoggedInEvent += OnUserLoggedIn;
        }

        void OnValidate()
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<NetworkTransform>().clientAuthority = true;
        }

        public override void OnStartLocalPlayer()
        {
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition = new Vector3(0, 0, 0);

            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            height = GetComponent<CapsuleCollider>().height;
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        void Update()
        {
            if (!isLocalPlayer) { return; }

            isGrounded = Physics.Raycast(transform.position, Vector3.down, height * 0.5f + 0.2f);

            GetMoveInput();
            GetJumpInput();

            // Limita velocidade horizontal
            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            if (flatVelocity.magnitude > moveSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
            }

            if (isGrounded)
                rb.drag = dragForce;
            else
                rb.drag = 0f;

            FollowMouseView();
        }

        private void GetMoveInput()
        {
            hInput = Input.GetAxisRaw("Horizontal");
            vInput = Input.GetAxisRaw("Vertical");
        }

        private void GetJumpInput()
        {
            if (Input.GetKey(jumpKey) && readyToJump && isGrounded)
            {
                readyToJump = false;
                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        private void MovePlayer()
        {
            Vector3 heading = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
            moveDirection = heading * vInput + transform.right * hInput;

            if (isGrounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            else if (!isGrounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

            Camera.main.transform.position = transform.position;
        }

        private void FollowMouseView()
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation = Mathf.Clamp(xRotation - mouseY, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

            Camera.main.transform.rotation = transform.rotation;
        }

        private void Jump()
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            readyToJump = true;
        }


        public override void OnStartClient() {
            try
            {
                vivoxVoiceManager.Login();
            }
            catch (System.Exception)
            {
                Debug.LogError("<color=green>VivoxVoice: </color>: Falha no Login");
            }
        }

        public override void OnStopClient()
        {
            vivoxVoiceManager.Logout();
        }


        private void JoinLobbyChannel()
        {
            // Do nothing, participant added will take care of this
            vivoxVoiceManager.JoinChannel(LobbyChannelName, ChannelType.NonPositional, VivoxVoiceManager.ChatCapability.AudioOnly);
        }

        private void OnUserLoggedIn()
        {
            var lobbychannel = vivoxVoiceManager.ActiveChannels.FirstOrDefault(ac => ac.Channel.Name == LobbyChannelName);
            if ((vivoxVoiceManager && vivoxVoiceManager.ActiveChannels.Count == 0) || lobbychannel == null)
            {
                JoinLobbyChannel();
            }
            else
            {
                if (lobbychannel.AudioState == ConnectionState.Disconnected)
                {
                    // Ask for hosts since we're already in the channel and part added won't be triggered.

                    lobbychannel.BeginSetAudioConnected(true, true, ar =>
                    {
                        Debug.Log("Now transmitting into lobby channel");
                    });
                }

            }
        }
    }

    
}