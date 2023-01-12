using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class SetupScores : MonoBehaviour
{
    void Start()
    {
        PhotonNetwork.LocalPlayer.SetScore(0);
        var hashTable = new Hashtable();
        hashTable.Add("Deaths", 0);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashTable);
    }
}