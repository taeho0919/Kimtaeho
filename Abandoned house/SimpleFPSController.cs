using UnityEngine;

public class SimpleFPSController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public Light flashlight;
    public Camera playerCamera;
    public float jumpForce = 5.0f;

    private float rotationX = 0;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    void Start()
    {
       
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
        if (playerCamera == null)
        {
            Debug.LogError("Camera not assigned. Please assign a camera in the inspector.");
        }

        
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        capsuleCollider = GetComponent<CapsuleCollider>();
        if (capsuleCollider == null)
        {
            capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
        }

        
        rb.freezeRotation = true;

        
        if (flashlight == null)
        {
            Debug.LogError("Flashlight not assigned. Please assign a flashlight in the inspector.");
        }
    }

    void Update()
    {
        Move();
        LookAround();
        ToggleFlashlight();
    }

    void Move()
    {
        float moveForward = Input.GetKey(KeyCode.W) ? 1 : (Input.GetKey(KeyCode.S) ? -1 : 0);
        float moveSideways = Input.GetKey(KeyCode.D) ? 1 : (Input.GetKey(KeyCode.A) ? -1 : 0);

        Vector3 moveDirection = transform.right * moveSideways + transform.forward * moveForward;
        Vector3 newPosition = rb.position + moveDirection * moveSpeed * Time.deltaTime;
        
        
        rb.MovePosition(newPosition);

        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void LookAround()
    {
        if (playerCamera == null) return;

        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + mouseX, 0);
    }

    void ToggleFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.F) && flashlight != null)
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }

    bool IsGrounded()
    {
        
        return Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }
}