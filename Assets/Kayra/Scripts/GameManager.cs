using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] TextMeshProUGUI textMeshOutput;
    [SerializeField] GridScript gridScriptInScene;
    public int rotationRate = 0;
    public int rotateCounter = 0;
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

    public void CheckGameOverForButtons() => CheckGameOver();
    public bool CheckGameOver()
    {
        Shape[] shapesInScene = FindObjectsByType<Shape>(FindObjectsSortMode.None);
        List<GridSquareScript[]> GSSGrid = new List<GridSquareScript[]>();

        for (int row = 0; row < gridScriptInScene.rows; row++)
        {
            GSSGrid.Add(gridScriptInScene.GetRowAsScript(row));
        }

        // Debug the rows
        if (textMeshOutput != null)
        {
            textMeshOutput.text = "";
            foreach (Shape shape in shapesInScene)
            {
                ShapeDataScript.Row[] rotatedBoard = RotateBoard(shape.currentShapeData.board, rotationRate);
                foreach (ShapeDataScript.Row r in rotatedBoard)
                {
                    foreach (bool b in r.columns)
                    {
                        textMeshOutput.text += b ? "[]" : "x";
                    }
                    textMeshOutput.text += "<br>";
                }
                textMeshOutput.text += "<br>";
            }
        }

        // For every shape in the scene
        foreach (Shape shape in shapesInScene)
        {
            // Rows of the shape
            ShapeDataScript.Row[] rows = RotateBoard(shape.currentShapeData.board, rotationRate);

            // For every gridSquare in grid
            for (int gridY = 0; gridY < GSSGrid.Count; gridY++)
            {
                for (int gridX = 0; gridX < GSSGrid[gridY].Length; gridX++)
                {
                    bool gridFailed = false;

                    // For every bool in shape's shapeData --Y--
                    for (int shapeY = 0; shapeY < rows.Length; shapeY++)
                    {
                        // OUT OF RANGE Y
                        if (gridY + shapeY >= GSSGrid.Count)
                        {
                            gridFailed = true;
                            break;
                        }

                        // For every bool in shape's shapeData --X--
                        for (int shapeX = 0; shapeX < rows[shapeY].columns.Length; shapeX++)
                        {
                            // OUT OF RANGE X
                            if (gridX + shapeX >= GSSGrid[gridY + shapeY].Length)
                            {
                                gridFailed = true;
                                break;
                            }

                            // We do not have to check here
                            if (rows[shapeY].columns[shapeX] == false)
                            {
                                continue;
                            }

                            // Here is occupied
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

        GameIsOver();
        return true;
    }
    void CheckTwo()
    {
        
    }
    void GameIsOver()
    {
            textMeshOutput.text = "GAME OVER";
    }
    // Rotate board function
    private ShapeDataScript.Row[] RotateBoard(ShapeDataScript.Row[] board, int rotateRate)
    {
        int rotationCount = rotateRate / 90;
        ShapeDataScript.Row[] rotatedBoard = board;

        for (int i = 0; i < rotationCount; i++)
        {
            rotatedBoard = RotateBoard90Degrees(rotatedBoard);
        }

        return rotatedBoard;
    }

    // Rotate board 90 degrees function
    private ShapeDataScript.Row[] RotateBoard90Degrees(ShapeDataScript.Row[] board)
    {
        int rowCount = board.Length;
        int columnCount = board[0].columns.Length;       
        ShapeDataScript.Row[] rotatedBoard = new ShapeDataScript.Row[columnCount];

        for (int i = 0; i < columnCount; i++)
        {
            bool[] rotatedColumns = new bool[rowCount];
            for (int j = 0; j < rowCount; j++)
            {
                rotatedColumns[j] = board[rowCount - 1 - j].columns[i];
            }
            rotatedBoard[i] = new ShapeDataScript.Row(rotatedColumns.Length);
            rotatedBoard[i].columns = rotatedColumns;
        }
        rotateCounterFunc();
        return rotatedBoard;
    }

    public void rotateCounterFunc()
    {
        rotateCounter = rotateCounter + 90;
        if (rotateCounter == 360)
        {
            rotateCounter = 0;
        }
    }
}