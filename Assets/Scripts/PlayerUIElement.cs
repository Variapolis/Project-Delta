using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public abstract class PlayerUIElement : MonoBehaviourPunCallbacks
{
    public abstract Player Player { get; set; }
}