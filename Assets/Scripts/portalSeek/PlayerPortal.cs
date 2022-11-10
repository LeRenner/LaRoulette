using Mirror;
using UnityEngine;

// namespace Mirror.Examples.AdditiveLevels
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerPortal : NetworkBehaviour
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

    [Header("Fire")]
    public Weapon weapon;
    public PortalManager portalMan;


    KeyCode jumpKey = KeyCode.Space;

    float hInput;
    float vInput;

    Vector3 moveDirection;
    Rigidbody rb;

    bool isGrounded;
    float height;

    void OnValidate()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        //GetComponent<NetworkTransform>().clientAuthority = true;
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
        GetFireInput();
        
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

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation - mouseY, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private void GetMoveInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
    }

    private void GetFireInput()
    {
        // checar se o player eh hider tbm

        if (Input.GetButtonDown("Fire1")) {
            Debug.Log("Left click!");

            GameObject bullet = Instantiate(weapon.weaponBullet, weapon.weaponFirePosition.position, weapon.weaponFirePosition.rotation);

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * weapon.weaponSpeed;
            gameObject.name = GetInstanceID().ToString();
            bullet.GetComponent<Bullet>().playerName = gameObject.name;
            bullet.GetComponent<Bullet>().Type = true;

            Destroy(bullet, weapon.weaponLife);
        }
        if (Input.GetButtonDown("Fire2")) {
            Debug.Log("Right click!");

            GameObject bullet = Instantiate(weapon.weaponBullet, weapon.weaponFirePosition.position, weapon.weaponFirePosition.rotation);

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * weapon.weaponSpeed;
            gameObject.name = GetInstanceID().ToString();
            bullet.GetComponent<Bullet>().playerName = gameObject.name;
            bullet.GetComponent<Bullet>().Type = false;

            Destroy(bullet, weapon.weaponLife);
        }
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
        else if (!isGrounded){

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
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
}
