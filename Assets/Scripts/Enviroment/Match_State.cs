using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match_State : MonoBehaviour
{
    public static bool GameRunning = false;

    [SerializeField] private GameObject UIPanel;

    public void StartGame()
    {
        if(PlayfabManager.nameInputField.text.Length > 0)
        {
            GameRunning = true;
        }
    }

    private void ResetGameState()
    {
        GameRunning = false;
    }

    private void OnEnable() 
    {
        Player_Movement.OnPlayerDeath += ResetGameState;    
        Player_Movement.OnPlayerDeath += ()=>UIPanel.SetActive(true);    
    }

    private void OnDisable() 
    {    
        Player_Movement.OnPlayerDeath-= ResetGameState;
    }
}
