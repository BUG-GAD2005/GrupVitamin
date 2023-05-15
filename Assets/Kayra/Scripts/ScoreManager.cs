using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;
    public int Score { get; private set; }

    [SerializeField] TextMeshProUGUI ScoreText;

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
    }

    public void AddScore(int amount)
    {
        if (amount <= 0) return;

        Score += amount;
        RefreshScoreText();
    }
    void RefreshScoreText()
    {
        if (ScoreText == null) return;

        ScoreText.text = "SCORE : " + Score;
    }
}
