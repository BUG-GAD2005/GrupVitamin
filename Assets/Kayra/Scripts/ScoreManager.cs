using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int Score { get; private set; }

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
    }

    public void AddScore(int amount)
    {
        if (amount <= 0) return;

        Score += amount;
    }
}
