using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public int enoughScore = 5;
    public List<GameObject> boostPrefabs;
    public Transform boostHolder;
    
    bool IsEnoughScore()
    {
        if(scoreManager.Score >= enoughScore)
        {
            enoughScore *= 2;
            return true;
        }
        else
        { 
            return false; 
        }
    }

    int GetRandomBoost()
    {
        int boostIndex = Random.Range(0, boostPrefabs.Count);

        return boostIndex;
    }

    public void GiveBoostToPlayer()
    {
        if(IsEnoughScore()) 
        {
            Instantiate(boostPrefabs[GetRandomBoost()], boostHolder);
        }
    }
}
