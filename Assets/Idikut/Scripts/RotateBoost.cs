using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBoost : MonoBehaviour
{
    //int rotateCount = GameManager.instance.rotationRate;
    public int rotateCount = 0;
    int RotatedCount = 0;

    void Start()
    {
        //rotateCount = GameManager.instance.rotationRate;
       
    }
    public void buttonRotate()
    {
        Rotate(90,true);
        RandomizeBoost randomizeBoost = GameObject.FindObjectOfType<RandomizeBoost>();
        randomizeBoost.rotateCount = 0;

    }
    public void Rotate(int rotationDegree, bool isCounter)
    {
        RotateCounter(true);       
        RotateParent(rotationDegree);
        RotateChilds(rotationDegree);       
        GameManager.instance?.CheckGameOver();
    }
    void RotateParent(int RotationDegree)
    {
        GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");
        foreach (GameObject shape in shapes)
        {
            shape.transform.Rotate(0, 0, -RotationDegree);
        }
    }
    void RotateChilds(int rotationDegree)
    {
        GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");
        foreach (GameObject shape in shapes)
        {
            foreach (Transform child in shape.transform)
            {
                child.transform.Rotate(0, 0, rotationDegree);
            }
        }
    } 
    void RotateCounter(bool isCounter)
    { 
        if(isCounter)
        {
            rotateCount = rotateCount + 90;
            if(rotateCount == 360)
            {
                rotateCount = 0;
            }
            GameManager.instance.rotationRate = rotateCount;
        }
        
    }
    public void ResetRotateCount()
    {
        Rotate(GameManager.instance.rotationRate, false);
        Debug.Log("ResetRotateCount");
        
        //Rotate(90,true);
        //get RandomizeBoost's rotateCount
        RandomizeBoostFixer();
        GameManager.instance?.CheckGameOver();

    }

    void RandomizeBoostFixer()
    {
        RandomizeBoost randomizeBoost = GameObject.FindObjectOfType<RandomizeBoost>();
        int randomizeBoostCount = randomizeBoost.RotateAdjustments();
        if(randomizeBoostCount == 0)
        {
            Rotate(180,true);
        }
        else if(randomizeBoostCount == 1)
        {
            Rotate(0,true);
        }
        else if(randomizeBoostCount == 2)//1
        {
            Rotate(0,true);
        }
        else if(randomizeBoostCount == 3)
        {
            Rotate(180,true);
        }
    }

}
