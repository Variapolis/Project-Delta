using UnityEngine;

public class BasicFollow : MonoBehaviour
{
    public Transform parent;
    [SerializeField] private float speed;
    private Vector3 _offset;

    private void Start()
    {
        if (!parent) return;
        _offset = transform.position - parent.position;
    }

    private void Update()
    {
        if (!parent) return;
        transform.position = parent.position + _offset;
    }
}