using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeBoost : MonoBehaviour
{
    RandomizeShape randomizeShape;

    private void Start()
    {
        randomizeShape = GameObject.FindObjectOfType<RandomizeShape>();
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
}
