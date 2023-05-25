using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeBoost : MonoBehaviour
{
    ShapeManager randomizeShape;

    private void Start()
    {
        randomizeShape = GameObject.FindObjectOfType<ShapeManager>();
    }
    public void RandomizeShapesAgain()
    {
        GameManager.instance.ResetShapesRotation();
        DestroyShapeObjects();   
        randomizeShape.shapesList.Clear();
        randomizeShape.InstantiateShapePrefab(ShapeListCount());

        DestroyCurrentRandomizeBoost();
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

    void DestroyCurrentRandomizeBoost()
    {
        Destroy(gameObject);
    }
}
