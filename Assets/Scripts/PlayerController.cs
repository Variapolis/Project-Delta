using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private float moveSpeed;
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private Transform orientation;
    [SerializeField] private float groundDrag;
    [SerializeField] private float rotationSpeed;

    [Header("Ground Check")] [SerializeField]
    private float playerHeight;

    [SerializeField] LayerMask groundMask;
    private bool isGrounded; // BUG: Doesn't work

    [Header("References")] private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Transform animationAdjustment;

    private Rigidbody rb;
    [SerializeField] private Animator _animator;

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
        // orientation.forward = cameraHolder.forward.normalized;
        isAiming = Input.GetKey(KeyCode.Mouse1);
        if (isAiming) TurnToCrosshair();
        else if (moveDirection.x != 0f || moveDirection.z != 0f) FaceForward();
        Animate();
    }

    void Animate()
    {
        if (isAiming)
        {
            var fromToRotation = Quaternion.FromToRotation(orientation.forward, cameraHolder.forward);
            var input = new Vector3(horizontalInput, 0, verticalInput).normalized;
            var movement = fromToRotation * input;
            _animator.SetFloat("MovementRight", movement.x);
            _animator.SetFloat("MovementForward", movement.z);
            return;
        }
        _animator.SetFloat("MovementForward", Mathf.Max(Mathf.Abs(verticalInput), Mathf.Abs(horizontalInput)));
        _animator.SetFloat("MovementRight", 0f);
    }

    private void TurnToCrosshair()
    {
        var crosshairPos = _crosshairController.worldPosition;
        var lookDir = Quaternion.LookRotation(
            new Vector3(crosshairPos.x, orientation.position.y, crosshairPos.z) - orientation.position);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, lookDir.normalized, Time.deltaTime * rotationSpeed);
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


        var inputDir = cameraHolder.forward * verticalInput + cameraHolder.right * horizontalInput;
        
        transform.forward = Vector3.Slerp(transform.forward, inputDir.normalized,
            Time.deltaTime * rotationSpeed);
        // TODO: Make this face left and right and back based on WASD input and based on the camera direction, and then play the forward animation.
        // TODO: UNLESS aiming, in which case, use directional controls and animations.
    }

    void MovePlayer()
    {
        moveDirection = cameraHolder.forward * verticalInput + cameraHolder.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}