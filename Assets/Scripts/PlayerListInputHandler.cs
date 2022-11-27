using UnityEngine;

public class PlayerListInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject playerList;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){ playerList.SetActive(true);}
        if(Input.GetKeyUp(KeyCode.Tab)) playerList.SetActive(false);
    }
}