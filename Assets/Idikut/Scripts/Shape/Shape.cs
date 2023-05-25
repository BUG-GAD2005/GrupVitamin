using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class Shape : MonoBehaviour
{
    public bool isMovable { get; private set; } = true;

    public GameObject squareShapeImage;
    public ShapeDataScript currentShapeData;
    public List<GameObject> _currentShape = new List<GameObject>();

    public bool TryPlaceShape()
    {
        List<GridSquareScript> gridSquareScripts = new List<GridSquareScript>();
        List<ShapeSquare> shapeSquares = new List<ShapeSquare>();

        foreach(GameObject shapeSquare in _currentShape)
        {
            if(shapeSquare.TryGetComponent(out ShapeSquare ss) == false)
            {
                return false;
            }

            if(ss.CheckForGridSquareToOccupy(out GridSquareScript gss) == false)
            {
                return false;
            }

            gridSquareScripts.Add(gss);
            shapeSquares.Add(ss);
        }

        for (int i = 0; i < gridSquareScripts.Count; i++)
        {
            GridSquareScript gss = gridSquareScripts[i];
            ShapeSquare ss = shapeSquares[i];

            gss.Occupy(ss.transform);
        }

        if(gridSquareScripts.Count > 0)
        {
            gridSquareScripts[0].ParentGridScript.CheckForMatches();
        }

        Destroy(gameObject);

        //GameManager.instance?.CheckGameOver();

        return true;
    } 

    public void RequestNewShape(ShapeDataScript shapeData)
    {
        CreateShape(shapeData);
    }
    public void CreateShape(ShapeDataScript shapeData)
    {
        currentShapeData = shapeData;
        var totalSquareNumber = GetNumberOfSquares(shapeData);
        while(_currentShape.Count < totalSquareNumber)
        {           
            _currentShape.Add(Instantiate(squareShapeImage, transform)as GameObject);
        }
        foreach(var square in _currentShape)
        {
            square.gameObject.transform.position = Vector3.zero;
            square.gameObject.SetActive(false);
        }

        var squareRect = squareShapeImage.GetComponent<RectTransform>();
        var moveDistance = new Vector2(squareRect.rect.width* squareRect.localScale.x, squareRect.rect.height* squareRect.localScale.y);

        int currentIndexList = 0;
        //Set positions to form final shape
        for(var row = 0 ; row < shapeData.rows; row++)
        {
            for(var column=0; column< shapeData.columns; column++)
            {
                if(shapeData.board[row].columns[column])
                {
                    _currentShape[currentIndexList].SetActive(true);
                    _currentShape[currentIndexList].GetComponent<RectTransform>().localPosition = new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance),
                    GetYPositionForShapeSquare(shapeData, row, moveDistance));

                    currentIndexList++;
                }
            }
        }
    }
    private float GetYPositionForShapeSquare(ShapeDataScript shapeData, int row, Vector2 moveDistance)
    {
        float shiftOnY = 0f;
        if(shapeData.rows >1)
        {
            if(shapeData.rows %2 != 0)
            {
                var middleSquareIndex = (shapeData.rows - 1) / 2;
                var multiplier = (shapeData.rows - 1) / 2 - row;

                if(row < middleSquareIndex)//move it on minus
                {
                    shiftOnY = moveDistance.y;
                    shiftOnY *= -multiplier;
                }
                else if(row > middleSquareIndex)//move it on plus
                {
                    shiftOnY = moveDistance.y * -1;
                    shiftOnY *= multiplier;
                }
            }
            else
            {
                var middleSquareIndex2 = (shapeData.rows == 2)? 1 : shapeData.rows / 2; 
                var middleSquareIndex1 = (shapeData.rows == 2)? 0 : shapeData.rows - 2;
                var multiplier = shapeData.rows/2;

                if(row == middleSquareIndex1 || row == middleSquareIndex2)
                {
                    if(row == middleSquareIndex2)
                        shiftOnY = moveDistance.y/2 * -1;
                    if(row == middleSquareIndex1)
                        shiftOnY = (moveDistance.y/2);
                }
                if(row < middleSquareIndex1 && row < middleSquareIndex2)//move it on minus
                {
                    shiftOnY = moveDistance.y;
                    shiftOnY *= multiplier;
                }
                else if(row > middleSquareIndex1 && row > middleSquareIndex2)//move it on plus
                {
                    shiftOnY = moveDistance.y * -1;
                    shiftOnY *= multiplier;
                }
            }
        }
        return shiftOnY;
    }
    private float GetXPositionForShapeSquare(ShapeDataScript shapeData, int column, Vector2 moveDistance)
    {
        float shiftOnX = 0f;
        if(shapeData.columns > 1)//vertical position calculate
        {
            if(shapeData.columns % 2 != 0)
            {
                var middleSquareIndex = (shapeData.columns - 1) / 2;
                var multiplier = (shapeData.columns - 1) / 2 - column;
                if(column < middleSquareIndex)//move it on the negative
                {
                    shiftOnX = moveDistance.x * -1;
                    shiftOnX *= multiplier;
                }
                else if(column > middleSquareIndex)//move it on plus
                {
                    shiftOnX = moveDistance.x;
                    shiftOnX *= -multiplier; 
                }
            }
            else
            {
                var middleSquareIndex2 = (shapeData.columns == 2)? 1 : shapeData.columns / 2;
                var middleSquareIndex1 = (shapeData.columns == 2)? 0 : shapeData.columns - 1;
                var multiplier = shapeData.columns/2;
                if(column == middleSquareIndex1 || column == middleSquareIndex2)
                {
                    if(column == middleSquareIndex2)
                        shiftOnX = moveDistance.x/2;
                    if(column == middleSquareIndex1)
                        shiftOnX = (moveDistance.x/2)*-1;

                }

                if(column < middleSquareIndex1 && column < middleSquareIndex2)//move it on the negative
                {
                    shiftOnX = moveDistance.x * -1;
                    shiftOnX *= multiplier;
                }
                else if(column > middleSquareIndex1 && column > middleSquareIndex2)//move it on plus
                {
                    shiftOnX = moveDistance.x;
                    shiftOnX *= multiplier; 
                }
            }
        }
        return shiftOnX;
    }
    private int GetNumberOfSquares(ShapeDataScript shapeData)
    {
        int number = 0;

        foreach(var rowData in shapeData.board)
        {
            foreach(var active in rowData.columns)
            {
                if(active)
                    number++;
            }
        }
        return number;
    }

}
