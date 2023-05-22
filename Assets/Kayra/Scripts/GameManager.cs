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

    public void CheckGameOverForButtons() 
    {
        string temp = CheckGameOver().ToString();
        textMeshOutput.text += temp;
    }
    public bool CheckGameOver()
    {
        Shape[] shapesInScene = FindObjectsByType<Shape>(FindObjectsSortMode.None);
        List<ShapeDataScript.Row[]> shapeRowsInScene = new List<ShapeDataScript.Row[]>();

        List<GridSquareScript[]> GSSGrid = new List<GridSquareScript[]>();

        for (int row = 0; row < gridScriptInScene.rows; row++)
        {
            GSSGrid.Add(gridScriptInScene.GetRowAsScript(row));
        }

        //debug the rows
        textMeshOutput.text = "";
        foreach (Shape shape in shapesInScene)
        {
            foreach (ShapeDataScript.Row r in shape.currentShapeData.board)
            {
                foreach (bool b in r.columns)
                {
                    textMeshOutput.text += b ? "[]" : "x";
                }
                textMeshOutput.text += "<br>";
            }
            textMeshOutput.text += "<br>";
        }

        //for every shape in the scene
        foreach (Shape shape in shapesInScene)
        {
            //rows of the shape
            ShapeDataScript.Row[] rows = shape.currentShapeData.board;


            //for every gridSquare in grid
            for (int gridY = 0; gridY < GSSGrid.Count; gridY++){
                for (int gridX = 0; gridX < GSSGrid[gridY].Length; gridX++)
                {
                    bool gridFailed = false;


                    //for every bool in shape's shapeData --Y--
                    for (int shapeY = 0; shapeY < rows.Length; shapeY++)
                    {
                        //OUT OF RANGE Y
                        if (gridY + shapeY >= GSSGrid.Count)
                        {
                            gridFailed = true;
                            break;
                        }

                        //for every bool in shape's shapeData --X--
                        for (int shapeX = 0; shapeX < rows[shapeY].columns.Length; shapeX++)
                        {

                            //OUT OF RANGE X
                            if(gridX + shapeX >= GSSGrid[gridY+shapeY].Length)
                            {
                                gridFailed = true;
                                break;
                            }

                            //We do not have to check here
                            if(rows[shapeY].columns[shapeX] == false)
                            {
                                continue;
                            }

                            //Here is occupied
                            if (GSSGrid[gridY + shapeY][gridX + shapeX].isOccupied)
                            {
                                gridFailed = true;
                                break;
                            }
                        }

                        if (gridFailed) break;
                    }


                    if (gridFailed == false) return false;
                }
            }
        }

        return true;
    }
}