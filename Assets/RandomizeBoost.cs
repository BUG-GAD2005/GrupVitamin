using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeBoost : MonoBehaviour
{
    public List<Shape> shapesList;

    RandomizeShape randomizeShape;


    private void Start()
    {
        randomizeShape = GameObject.FindObjectOfType<RandomizeShape>();
    }
    public void RandomizeShapesAgain()
    {
        GetShapesOnSceneAndDestroy();
        randomizeShape.shapesList = shapesList;
        randomizeShape.GetRandomShapeIndex(shapesList);
        //Destroy(gameObject);

    }
    void GetShapesOnSceneAndDestroy()
    {
        shapesList.Clear();

        Shape[] shapeObjects = GameObject.FindObjectsOfType<Shape>();

        foreach (var shapeObject in shapeObjects)
        {
            shapesList.Add(shapeObject);
            //Destroy(shapeObject);
        }
    }

    //int shapeCount()
    //{
    //    GetShapesOnSceneAndDestroy();
    //    return shapesList.Count;
    //}
}
