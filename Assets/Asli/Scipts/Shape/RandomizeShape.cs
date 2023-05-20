using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class RandomizeShape : MonoBehaviour
{
    public List<ShapeDataScript> shapeDataScriptsList;
    public List<Shape> shapesList;

    public GameObject shapeHolder;
    public GameObject shapePrefab;

    private void Start()
    {
        shapeHolder = GameObject.Find("Shape Holder");
        AddShapesToList();
    }

    private void Update()
    {
        InstantiateShapePrefab();
    }

    public void InstantiateShapePrefab()
    {
        if (!IsThereAnyShape())
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(shapePrefab, shapeHolder.transform);
            }
            GetShapesOnScene();
            GetRandomShapeIndex(shapesList);
        }
    }

    
    void AddShapesToList()
    {
        ShapeDataScript[] shapes = Resources.LoadAll<ShapeDataScript>("Shapes");

        foreach (var shape in shapes)
        {
            shapeDataScriptsList.Add(shape);
        }
    }

    void GetShapesOnScene()
    {
        shapesList.Clear();

        Shape[] shapeObjects = GameObject.FindObjectsOfType<Shape>();

        foreach (var shapeObject in shapeObjects) 
        {
            shapesList.Add(shapeObject);
        }
    }

    public void GetRandomShapeIndex(List<Shape> shapesList)
    {
        //GetShapesOnScene();

        foreach (var shape in shapesList)
        {
            var randomShapeIndex = Random.Range(0, shapeDataScriptsList.Count);
            shape.RequestNewShape(shapeDataScriptsList[randomShapeIndex]);           
        }
    }

    bool IsThereAnyShape()
    {
        if (shapeHolder.transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
