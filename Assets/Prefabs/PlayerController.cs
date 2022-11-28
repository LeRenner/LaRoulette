using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mirror.Examples.AdditiveLevels
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(NetworkTransform))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : NetworkBehaviour
    {
        public int number;
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
        


	    Vector2 movement;

        float hInput;
        float vInput;

        Vector3 moveDirection;
        Rigidbody rb;
        bool pressedjump = false;
        bool isGrounded;
        

        float height;
        float mouseAxisX;
        float mouseAxisY;

        void OnValidate()
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<NetworkTransform>().clientAuthority = true;
        }

        public override void OnStartLocalPlayer()
        {
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition = new Vector3(0, 0, 0);

            rb = this.GetComponent<Rigidbody>();
            Debug.Log(rb);
            rb.freezeRotation = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            height = GetComponent<CapsuleCollider>().height;
        }

       /* void FixedUpdate()
        {
            //if (!isLocalPlayer) { return; }
            MovePlayer();
        }*/

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
            MovePlayer();

        }

        private void GetMoveInput()
        {
            hInput = movement.x;
            vInput = movement.y;
        }

        private void GetJumpInput()
        {
            if (pressedjump && readyToJump && isGrounded)
            {
                readyToJump = false;
                pressedjump = false;
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
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }
            Camera.main.transform.position = transform.position;
        }

        private void FollowMouseView()
        {
            float mouseX = mouseAxisX * Time.deltaTime * sensX;
            float mouseY = mouseAxisY * Time.deltaTime * sensY;

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

    	public void Movement(InputAction.CallbackContext value) {
            movement = value.ReadValue<Vector2>();
    	}

    	public void Jump(InputAction.CallbackContext value) {
            if (value.started){
                pressedjump = true;
            }
    	}

    	public void MouseX(InputAction.CallbackContext value) {
            mouseAxisX = value.ReadValue<float>();
    	}

     	public void MouseY(InputAction.CallbackContext value) {
            mouseAxisY = value.ReadValue<float>();
    	}       

    }
}