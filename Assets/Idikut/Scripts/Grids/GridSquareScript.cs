using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquareScript : MonoBehaviour
{
    public Image normalImage;
    public List<Sprite> normalImages;

    public void SetImage(bool SetFirstImage)
    {
        if(SetFirstImage)       
            normalImage.sprite = normalImages[0];

        else
            normalImage.sprite = normalImages[1];
    }
}
