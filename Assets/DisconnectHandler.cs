using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Cursor = UnityEngine.Cursor;

public class DisconnectHandler : MonoBehaviourPunCallbacks
{
    public override void OnLeftRoom()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }
}
