using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }
    private void Update() => InputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
}