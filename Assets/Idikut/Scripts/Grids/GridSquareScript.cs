using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquareScript : MonoBehaviour
{
    public bool isOccupied { get; private set; }

    public GridScript ParentGridScript;
    public Image normalImage;
    public List<Sprite> normalImages;

    Transform occupyingTransform;

    public void SetImage(bool SetFirstImage)
    {
        if(SetFirstImage)       
            normalImage.sprite = normalImages[0];

        else
            normalImage.sprite = normalImages[1];
    }

    public void Occupy(Transform transformToOccupyWith)
    {
        if (isOccupied) return;

        ScoreManager.instance?.AddScore(1);

        transformToOccupyWith.transform.SetParent(transform);
        transformToOccupyWith.transform.localPosition = Vector2.zero;
        occupyingTransform = transformToOccupyWith;

        isOccupied = true;
    }
    public void Unoccupy()
    {
        if (isOccupied == false) return;

        Destroy(occupyingTransform.gameObject);

        isOccupied = false;
    }
}
