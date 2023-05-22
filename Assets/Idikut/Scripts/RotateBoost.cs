using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBoost : MonoBehaviour
{
    public void Rotate()
    {       
        RotateParent();
        RotateChilds();       
        GameManager.instance?.CheckGameOver();
    }
    void RotateParent()
    {
        GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");
        foreach (GameObject shape in shapes)
        {
            shape.transform.Rotate(0, 0, 90);
        }
    }
    void RotateChilds()
    {
        GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");
        foreach (GameObject shape in shapes)
        {
            foreach (Transform child in shape.transform)
            {
                child.transform.Rotate(0, 0, -90);
            }
        }
    }
}
