using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBoost : MonoBehaviour
{
    public int rotateCount = 0;
    ShapeManager randomizeShape;

    void Start()
    {
        randomizeShape = FindAnyObjectByType<ShapeManager>();
        rotateCount = 0;
    }
    private void FixedUpdate()
    {
        DestroyCurrentRotateBoost();
    }
    public void ButtonRotate()
    {
        randomizeShape.isUsingRotateBoost = true;
        Rotate(90,true);
    }
    public void Rotate(int rotationDegree, bool isCounter)
    {
        RotateCounter(true);       
        RotateParent(rotationDegree);
        RotateChilds(rotationDegree);
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

    void DestroyCurrentRotateBoost()
    {
        if (randomizeShape.canDestroyRotateBoost)
        {
            randomizeShape.canDestroyRotateBoost = false;
            Destroy(gameObject);
        }
    }
}
