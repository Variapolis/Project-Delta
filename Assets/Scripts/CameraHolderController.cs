using UnityEngine;

public class CameraHolderController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float speed;
    private Vector3 _offset;

    private void Start()
    {
        if (!target) return;
        _offset = transform.position - target.position;
    }

    private void Update()
    {
        if (!target) return;
        transform.position = target.position + _offset;
    }
}