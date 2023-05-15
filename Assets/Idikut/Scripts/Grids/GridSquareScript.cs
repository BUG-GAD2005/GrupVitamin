using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquareScript : MonoBehaviour
{
    public bool isOccupied { get; private set; }

    public Image normalImage;
    public List<Sprite> normalImages;

    public void SetImage(bool SetFirstImage)
    {
        if(SetFirstImage)       
            normalImage.sprite = normalImages[0];

        else
            normalImage.sprite = normalImages[1];
    }

    public void Occupy(/*ref Transform transformToOccupyWith*/)
    {
        if (isOccupied) return;

        //transformToOccupyWith.parent = transform;
        //transformToOccupyWith.localPosition = Vector3.zero;

        isOccupied = true;
    }
}
