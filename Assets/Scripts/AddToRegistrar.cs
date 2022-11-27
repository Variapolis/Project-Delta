using UnityEngine;
using Zenject;

public class AddToRegistrar : MonoBehaviour
{
    [Inject] private Registrar _registrar;
    
    private void OnEnable() => _registrar += this;
    private void OnDisable() => _registrar -= this;
}
