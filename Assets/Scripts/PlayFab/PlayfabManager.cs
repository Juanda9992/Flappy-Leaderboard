using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using TMPro;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    [SerializeField] private Transform parentObj;
    [SerializeField] private GameObject prefabObject;
    public static TMP_InputField nameInputField;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject mainMenuPanel;
    private void Start() 
    {
        startButton.onClick.AddListener(()=>{
            if(nameInputField.text.Length > 0){
                mainMenuPanel.SetActive(false);
            }
            });
        nameInputField = GameObject.FindObjectOfType<TMP_InputField>();
        Login();    
    }
    private void Login()
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
        PlayFabClientAPI.LoginWithCustomID(request,OnSuccess,OnError);
    }

    private void OnSuccess(LoginResult result)
    {
        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null)
        {
            name =result.InfoResultPayload.PlayerProfile.DisplayName;
        }
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError("Cant create account");
        error.GenerateErrorReport();
    }

    public void SendScoreToLeaderBoard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Highest Scores",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request,OnLeaderBoardChanged,OnError);
    }

    private void OnLeaderBoardChanged(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Succesfully sended");
    }

    [ContextMenu("Test")]
    public void SendRandomScore()
    {
        SendScoreToLeaderBoard(Random.Range(1,100));
    }

    [ContextMenu("Test LeaderBoard")]
    public void GetLeader()
    {
        GetLeaderBoard();
    }

    public void GetLeaderBoard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName =  "Highest Scores",
            StartPosition = 0,
            MaxResultsCount = 10   
        };

        PlayFabClientAPI.GetLeaderboard(request,OnGetLeaderBoard,OnError);
    }

    private void OnGetLeaderBoard(GetLeaderboardResult result)
    {
        foreach(var player in result.Leaderboard)
        {
            GameObject instantiatedObject = Instantiate(prefabObject,parentObj);
            TextMeshProUGUI currentText = instantiatedObject.GetComponent<TextMeshProUGUI>();
            currentText.text = $"{player.Position + 1} {player.DisplayName} {player.StatValue}";

        }
    }

    public void UpdateUserName()
    {
        if(nameInputField.text.Length > 0)
        {
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = nameInputField.text
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request,OnUpdatedNameText,OnError);
        }
    }

    private void OnUpdatedNameText(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Changed");
    }

    public void ClearLeaderBoard()
    {
        foreach(Transform item in parentObj.GetComponentInChildren<Transform>())
        {
            Destroy(item.gameObject);
        }
    }
}
