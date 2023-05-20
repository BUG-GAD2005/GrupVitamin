using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] GridScript gridScriptInScene;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
    void Start()
    {
        
    }

    //public bool CheckGameOver()
    //{
    //    Shape[] shapesInScene = FindObjectsByType<Shape>(FindObjectsSortMode.None);
    //    List<ShapeDataScript.Row[]> shapeRowsInScene = new List<ShapeDataScript.Row[]>();

    //    foreach (Shape shape in shapesInScene)
    //    {
    //        shapeRowsInScene.Add(shape.currentShapeData.board);
    //    }

    //    return false;
    //}
}
