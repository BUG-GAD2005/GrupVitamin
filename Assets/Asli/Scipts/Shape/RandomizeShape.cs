using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class RandomizeShape : MonoBehaviour
{
    public List<ShapeDataScript> shapeDataScriptsList;
    public List<Shape> shapesList;

    private void Start()
    {
        AddShapesToList();
        GetRandomShapeIndex();
    }

    void AddShapesToList()
    {
        ShapeDataScript[] shapes = Resources.LoadAll<ShapeDataScript>("Shapes");

        foreach (var shape in shapes)
        {
            shapeDataScriptsList.Add(shape);
        }
    }

    void GetRandomShapeIndex()
    {
        foreach (var shape in shapesList)
        {
            var randomShapeIndex = Random.Range(0, shapeDataScriptsList.Count);
            shape.CreateShape(shapeDataScriptsList[randomShapeIndex]);
        }
    }
}
