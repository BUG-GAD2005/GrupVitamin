using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBoost : MonoBehaviour
{
    //int rotateCount = GameManager.instance.rotationRate;
    public int rotateCount = 0;
    bool doOnce = true;

    void Start()
    {
        //rotateCount = GameManager.instance.rotationRate;
       
    }
    public void buttonRotate()
    {
        Rotate(90,true);
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
        GameManager.instance?.CheckGameOver();
        doOnce = false;
    }  
}
