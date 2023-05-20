using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] TextMeshProUGUI textMeshOutput;
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

    public void CheckGameOverForButtons() {
        textMeshOutput.text = CheckGameOver().ToString();
    }
    public bool CheckGameOver()
    {
        Shape[] shapesInScene = FindObjectsByType<Shape>(FindObjectsSortMode.None);
        List<ShapeDataScript.Row[]> shapeRowsInScene = new List<ShapeDataScript.Row[]>();
        List<GridSquareScript> gridSquareScripts = gridScriptInScene.GridSquareScripts;

        List<GridSquareScript[]> gridSquareScriptGrid = new List<GridSquareScript[]>();

        for (int row = 0; row < gridScriptInScene.rows; row++)
        {
            gridSquareScriptGrid.Add(gridScriptInScene.GetRowAsScript(row));
        }

        foreach (Shape shape in shapesInScene)
        {
            shapeRowsInScene.Add(shape.currentShapeData.board);
        }

        for (int gridY = 0; gridY < gridSquareScriptGrid.Count; gridY++)
        {
            for (int gridX = 0; gridX < gridSquareScriptGrid[gridY].Length; gridX++)
            {
                //GridSquareScript currentGSS = gridSquareScriptGrid[gridY][gridX];

                foreach (ShapeDataScript.Row[] shapeRows in shapeRowsInScene)
                {
                    bool shapeFailed = false;

                    //rows of board
                    for (int rowY = 0; rowY < shapeRows.Length; rowY++)
                    {
                        //Y out of range
                        if (gridY + rowY >= gridScriptInScene.rows)
                        {
                            shapeFailed = true;
                            break;
                        }

                        //columns of board[y]
                        for (int rowX = 0; rowX < shapeRows[rowY].columns.Length; rowX++)
                        {
                            //X out of range
                            if (gridX + rowX >= gridScriptInScene.columns)
                            {
                                shapeFailed = true;
                                break;
                            }
                            //Shape data is false, we do not have to check here
                            if (shapeRows[rowY].columns[rowX] == false)
                            {
                                break;
                            }
                            //grid square is occupied
                            if (gridSquareScriptGrid[gridY + rowY][gridX + rowX].isOccupied)
                            {
                                shapeFailed = true;
                                break;
                            }
                        }

                        if (shapeFailed) break;
                    }

                    if (shapeFailed == false)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}