using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactionUIPicker : MonoBehaviour
{
    [SerializeField] private Sprite spyTeamIcon;
    [SerializeField] private Color spyColor;
    [SerializeField] private Sprite guardTeamIcon;
    [SerializeField] private Color guardColor;

    [Header("Elements to Change")]
    [SerializeField] private List<Image> images;

    [SerializeField] private List<TMP_Text> texts;

    private void Start()
    {
        var color = PhotonNetwork.LocalPlayer.GetPhotonTeam().Code == 1 ? spyColor : guardColor;
        foreach (var image in images)
        {
            image.sprite = PhotonNetwork.LocalPlayer.GetPhotonTeam().Code == 1 ? spyTeamIcon : guardTeamIcon;
            image.color = color;
        }

        foreach (var text in texts)
        {
            text.color = color;
            text.text = PhotonNetwork.LocalPlayer.GetPhotonTeam().Code == 1 ? "Spies" : "Guards";
        }
    }
}
