using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabRank : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform rowsParent;
    public GameObject leaderboard;
    public InputField nameInput;

    // 로그인 최초에만 하도록 수정

    private void Start()
    {
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while login");
        Debug.Log(error.GenerateErrorReport());
    }

    public void PressOK()
    {
        SubmitName();
    }

    public void SubmitName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInput.text
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OndisplayNameUpdate, OnError);
    }
    void OndisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("OK");
    }

    public void GetLeaderboard()
    {
        leaderboard.SetActive(true);
        var request = new GetLeaderboardRequest
        {
            StatisticName = "ScoreRank",
            StartPosition = 0,
            MaxResultsCount = 7
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    public void CloseLeaderboard()
    {
        leaderboard.SetActive(false);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
}
