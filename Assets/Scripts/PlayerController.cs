using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private float moveSpeed;
    [SerializeField] private Transform playerModel;
    [SerializeField] private Transform camera;
    [SerializeField] private Transform orientation;
    [SerializeField] private float groundDrag;
    [SerializeField] private float rotationSpeed;

    [Header("Ground Check")] [SerializeField]
    private float playerHeight;

    [SerializeField] LayerMask groundMask;
    private bool isGrounded;

    [Header("References")] private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    private Rigidbody rb;

    [SerializeField] private CrosshairController _crosshairController;
    private bool isAiming;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);
        GetInput();
        LimitSpeed();
        rb.drag = isGrounded ? groundDrag : 0f;
        orientation.forward = camera.forward.normalized;
        if (Input.GetKey(KeyCode.Mouse1))
        {
            TurnToCrosshair();
            return;
        }

        if (moveDirection.x != 0f || moveDirection.z != 0f) FaceForward();
    }

    private void TurnToCrosshair()
    {
        var crosshairPos = _crosshairController.worldPosition;
        var lookDir = Quaternion.LookRotation(
            new Vector3(crosshairPos.x, orientation.position.y, crosshairPos.z) - orientation.position);
        playerModel.rotation = Quaternion.Slerp(playerModel.rotation, lookDir, Time.deltaTime * rotationSpeed);
    }

    private void FixedUpdate() => MovePlayer();

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void LimitSpeed()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVelocity.magnitude > moveSpeed)
        {
            var limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    void FaceForward()
    {
        // var viewDir = transform.position - new Vector3(camera.position.x, transform.position.y, camera.position.z);


        var inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        playerModel.forward = Vector3.Slerp(playerModel.forward, inputDir.normalized,
            Time.deltaTime * rotationSpeed);
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}