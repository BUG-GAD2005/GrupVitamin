using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public int columns = 10, rows = 10;
    //public float squareSize = 0.1f;
    public float squareGap = 0.1f;
    public GameObject gridSquare;
    public Vector2 startPosition = new Vector2(0, 0);
    public float squareScale = 0.5f;
    public float everySquareScale = 0.0f;

    private Vector2 offset = new Vector2(0, 0);

    public List<GameObject> GridSquares => _gridSquares;
    private List<GameObject> _gridSquares = new List<GameObject>();

    public List<GridSquareScript> GridSquareScripts => _gridSquareScripts;
    private List<GridSquareScript> _gridSquareScripts = new List<GridSquareScript>();

    void Start()
    {
        CreateGrid();
    }

    public void UnoccupyAll()
    {
        foreach(GridSquareScript gss in _gridSquareScripts)
        {
            gss.Unoccupy();
        }
    }

    public void CheckForMatches()
    {
        List<GridSquareScript> matchedGridSquares = new List<GridSquareScript>();
        bool batchIsOk = true;
        int scoreToAdd = 0;

        //check for rows
        for (int i = 0; i < rows; i++)
        {
            foreach (GridSquareScript script in GetRowAsScript(i))
            {
                if (script.isOccupied == false)
                {
                    batchIsOk = false;
                    break;
                }
            }

            if (batchIsOk)
            {
                foreach (GridSquareScript script in GetRowAsScript(i))
                {
                    matchedGridSquares.Add(script);
                }

                scoreToAdd += 10;
            }

            batchIsOk = true;
        }

        batchIsOk = true;
        //check for columns
        for (int i = 0; i < columns; i++)
        {
            foreach (GridSquareScript script in GetColumnAsScript(i))
            {
                if (script.isOccupied == false)
                {
                    batchIsOk = false;
                    break;
                }
            }

            if (batchIsOk)
            {
                foreach (GridSquareScript script in GetColumnAsScript(i))
                {
                    matchedGridSquares.Add(script);
                }

                scoreToAdd += 10;
            }

            batchIsOk = true;
        }

        for (int i = 0; i < matchedGridSquares.Count; i++)
        {
            matchedGridSquares[i].Unoccupy();
        }
        ScoreManager.instance.AddScore(scoreToAdd);
    }

    void CreateGrid()
    {
        SpawnGridSquares();
        SetGridSquaresPosition();
    }
    void SpawnGridSquares()
    {
        int squareIndex = 0;
        for (int column = 0; column < rows; column++)
        {
            for (int row = 0; row < columns; row++)
            {
                _gridSquares.Add(Instantiate(gridSquare) as GameObject);
                _gridSquareScripts.Add(_gridSquares[_gridSquares.Count - 1].GetComponent<GridSquareScript>());

                _gridSquareScripts[_gridSquares.Count - 1].ParentGridScript = this;
                _gridSquares[_gridSquares.Count - 1].transform.SetParent(this.transform);
                _gridSquares[_gridSquares.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                //gridSquares[gridSquares.Count - 1].GetComponent<GridSquareScript>().SetImage(squareIndex % 2 == 0);
                _gridSquares[_gridSquares.Count - 1].name = (column * columns + row + 1).ToString();
                squareIndex++;
            }
        }
    }
    void SetGridSquaresPosition()
    {
        int columnNumber = 0;
        int rowNumber = 0;
        //bool reverseDirection = false;
        
        var squareRect = _gridSquares[0].GetComponent<RectTransform>().rect;
        offset.x = squareRect.width * squareScale + everySquareScale;
        offset.y = squareRect.height * squareScale + everySquareScale;

        foreach (GameObject square in _gridSquares)
        {
            if (columnNumber >= columns)
            {
                columnNumber = 0;
                rowNumber++;
                //reverseDirection = !reverseDirection;
            }

            var xPos = offset.x * columnNumber + (columnNumber * squareGap);
            var yPos = offset.y * rowNumber + (rowNumber * squareGap);

            //if (reverseDirection)
            //{
            //    xPos = offset.x * (columns - 1 - columnNumber) + ((columns - 1 - columnNumber) * squareGap);
            //}

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosition.x + xPos, startPosition.y - yPos);
            square.GetComponent<RectTransform>().localPosition = new Vector3(startPosition.x + xPos, startPosition.y - yPos, 0f);

            square.GetComponent<GridSquareScript>().SetImage(rowNumber % 2 == 1 ^ columnNumber % 2 == 1);

            columnNumber++;
        }
    }
    public GameObject[] GetColumn(int index)
    {
        if (_gridSquares.Count <= 0) return null;
        if (index >= columns) return null;

        GameObject[] GOsToReturn = new GameObject[rows];

        for(int i = 0; i < rows; i++)
        {
            GOsToReturn[i] = _gridSquares[index + i * columns];
        }

        return GOsToReturn;
    }
    public GameObject[] GetRow(int index)
    {
        if (_gridSquares.Count <= 0) return null;
        if (index >= rows) return null;

        GameObject[] GOsToReturn = new GameObject[columns];

        for (int i = 0; i < columns; i++)
        {
            GOsToReturn[i] = _gridSquares[index * columns + i];
        }

        return GOsToReturn;
    }
    public GridSquareScript[] GetColumnAsScript(int index)
    {
        if (_gridSquares.Count <= 0) return null;
        if (index >= columns) return null;

        GridSquareScript[] ScriptsToReturn = new GridSquareScript[rows];

        for (int i = 0; i < rows; i++)
        {
            ScriptsToReturn[i] = _gridSquareScripts[index + i * columns];
        }

        return ScriptsToReturn;
    }
    public GridSquareScript[] GetRowAsScript(int index)
    {
        if (_gridSquares.Count <= 0) return null;
        if (index >= rows) return null;

        GridSquareScript[] ScriptsToReturn = new GridSquareScript[columns];

        for (int i = 0; i < columns; i++)
        {
            ScriptsToReturn[i] = _gridSquareScripts[index * columns + i];
        }

        return ScriptsToReturn;
    }
}