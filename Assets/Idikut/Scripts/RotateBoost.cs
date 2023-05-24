using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBoost : MonoBehaviour
{
    //int rotateCount = GameManager.instance.rotationRate;
    int rotateCount = 0;

    void Start()
    {
        rotateCount = GameManager.instance.rotationRate;
    }

    public void Rotate()
    {
        RotateCounter();       
        RotateParent();
        RotateChilds();       
        GameManager.instance?.CheckGameOver();
    }
    void RotateParent()
    {
        GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");
        foreach (GameObject shape in shapes)
        {
            shape.transform.Rotate(0, 0, -90);
        }
    }
    void RotateChilds()
    {
        GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");
        foreach (GameObject shape in shapes)
        {
            foreach (Transform child in shape.transform)
            {
                child.transform.Rotate(0, 0, 90);
            }
        }
    } 
    void RotateCounter()
    {  
        rotateCount = rotateCount + 90;
        if(rotateCount == 360)
        {
            rotateCount = 0;
        }
        GameManager.instance.rotationRate = rotateCount;
    }
    
}
