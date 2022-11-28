using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AudioListenerInitializer : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    void Start()
    {
        if (photonView.IsMine) gameObject.AddComponent<AudioListener>();
    }
}
