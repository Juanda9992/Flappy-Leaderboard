using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score_Manager : MonoBehaviour
{
    [SerializeField] private int score;
    private int maxScore = 0;
    [SerializeField] private TextMeshProUGUI scoreText,maxScoreText;
    [SerializeField] private PlayfabManager manager;

    private void Start() 
    {
        maxScore = PlayerPrefs.GetInt("MaxScore");
        maxScoreText.text = $"Max Score: {maxScore}";
    }
    private void IncreaseScore()
    {
        score++;
        if(score > maxScore)
        {
            maxScore = score;
            maxScoreText.text = $"Max Score: {maxScore}";
        }
        UpdateScore();
    }
    private void ResetScore()
    {
        manager.SendScoreToLeaderBoard(score);
        score = 0;
        PlayerPrefs.SetInt("MaxScore",maxScore);
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    private void OnEnable() 
    {
        Player_Movement.OnPlayerTouchWall += IncreaseScore;    
        Player_Movement.OnPlayerDeath += ResetScore;
    }

    private void OnDisable()
    {
        Player_Movement.OnPlayerTouchWall -= IncreaseScore;    
        Player_Movement.OnPlayerDeath -= ResetScore;
    }
}
