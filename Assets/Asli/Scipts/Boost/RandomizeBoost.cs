using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeBoost : MonoBehaviour
{
    RandomizeShape randomizeShape;
    public int rotateCount = 0;

    private void Start()
    {
        randomizeShape = GameObject.FindObjectOfType<RandomizeShape>();
        RotateAdjustments();
    }
    public void RandomizeShapesAgain()
    {
        DestroyShapeObjects();   
        randomizeShape.shapesList.Clear();
        randomizeShape.InstantiateShapePrefab(ShapeListCount());
        
    }
   
    void DestroyShapeObjects()
    {
        for (int i = 0; i < ShapeListCount(); i++)
        {
            Destroy(randomizeShape.shapeHolder.transform.GetChild(i).gameObject);
        }
    }

    int ShapeListCount()
    {
        return randomizeShape.shapeHolder.transform.childCount;
    }


    public int RotateAdjustments()
    {
        rotateCount++;
        if (rotateCount == 5)
        {
            rotateCount = 0;
        }
        Debug.Log("Rotate Count: " + rotateCount);
        return rotateCount;
    }

}
