using Photon.Pun;
using UnityEngine;
using Zenject;

public class PlayerMovementController : MonoBehaviour
{
    [Inject] private readonly CameraHolderController _cameraHolder;
    [Inject] private readonly CrosshairController crosshairController;
    [Inject] private PhotonView photonView;

    [Header("Movement")] [SerializeField] private float moveSpeed;
    [SerializeField] private Transform orientation;
    [SerializeField] private float rotationSpeed;
    [Header("References")] private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _moveDirection;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    
    [SerializeField] private WeaponIKHandler _weaponIKHandler;
    private bool _isAiming;

    [SerializeField] private PlayerWeaponController weaponController;

    private void Reset() => rb = GetComponent<Rigidbody>();

    private void Update()
    {
        if (!photonView.IsMine) return;
        GetInput();
        LimitSpeed();
        _isAiming = Input.GetMouseButton(1);
        if (_isAiming)
        {
            _weaponIKHandler.Aim();
            if (Input.GetMouseButton(0) && photonView.IsMine) weaponController.FireWeapon();
        }
        else _weaponIKHandler.StopAiming();
        if (_isAiming) TurnToCrosshair();
        else if (_moveDirection.x != 0f || _moveDirection.z != 0f) FaceForward();
        Animate();
    }

    void Animate()
    {
        if (!_isAiming)
        {
            animator.SetFloat("MovementForward", Mathf.Max(Mathf.Abs(_verticalInput), Mathf.Abs(_horizontalInput)));
            animator.SetFloat("MovementRight", 0f);
            return;
        }

        var fromToRotation = Quaternion.FromToRotation(orientation.forward, _cameraHolder.transform.forward);
        var input = new Vector3(_horizontalInput, 0, _verticalInput).normalized;
        var movement = fromToRotation * input;
        animator.SetFloat("MovementRight", movement.x);
        animator.SetFloat("MovementForward", movement.z);
    }

    private void TurnToCrosshair()
    {
        var crosshairPos = crosshairController.worldPosition;
        var lookDir = Quaternion.LookRotation(
            new Vector3(crosshairPos.x, orientation.position.y, crosshairPos.z) - orientation.position);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, lookDir.normalized, Time.deltaTime * rotationSpeed);
    }

    private void FixedUpdate() => MovePlayer();

    private void GetInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
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
        var inputDir = _cameraHolder.transform.forward * _verticalInput + _cameraHolder.transform.right * _horizontalInput;
        transform.forward = Vector3.Slerp(transform.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    }

    void MovePlayer()
    {
        _moveDirection = _cameraHolder.transform.forward * _verticalInput + _cameraHolder.transform.right * _horizontalInput;
        rb.AddForce(_moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}