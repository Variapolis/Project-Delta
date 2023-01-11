using System.Collections;
using System.Collections.Generic;
using PlayFab;
using UnityEngine;
using PlayFab.ClientModels;

public class Leaderboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() => Login();

    private void Login()
    {
        SaveDataManager.TryLoadPlayerData(out var data);
        var request = new LoginWithCustomIDRequest()
        {
            CreateAccount = true,
            CustomId = data.guid
        };
        PlayFabClientAPI.LoginWithCustomID(request, PlayfabResultCallback, PlayfabErrorCallback );
    }

    private void PlayfabResultCallback(LoginResult result) => Debug.Log(result.ToJson());

    private void PlayfabErrorCallback(PlayFabError error) => Debug.Log(error.ErrorMessage);
}
