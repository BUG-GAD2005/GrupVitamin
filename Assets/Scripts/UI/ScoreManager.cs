using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    public BoostManager boostManager;
    public int Score { get; private set; }
    public int HighScore { get; private set; }

    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI HighScoreText;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
        
        RefreshScoreText();
        UpdateHighScore();
    }

    public void AddScore(int amount)
    {
        if (amount <= 0) return;

        Score += amount;
        RefreshScoreText();
        CheckHighScore();

        boostManager.GiveBoostToPlayer();
        
    }
    void RefreshScoreText()
    {
        if (ScoreText == null) return;

        ScoreText.text = "SCORE : " + Score;
    }

    public void CheckHighScore()
    {
        if (Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", Score);
            UpdateHighScore();
        }
    }
    void UpdateHighScore()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (HighScoreText != null)
        { HighScoreText.text = $"HighScore: {HighScore}"; }
    }
    public void ResetHighScore()
    {
        HighScore = 0;
        PlayerPrefs.SetInt("HighScore", 0);
        UpdateHighScore();
    }

}

