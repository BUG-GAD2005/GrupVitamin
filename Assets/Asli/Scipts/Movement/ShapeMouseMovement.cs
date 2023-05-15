using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShapeMouseMovement : MonoBehaviour
{
    Vector3 mousePositionOffset;

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();

    }

    private void OnMouseDrag() 
    { 
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }
}
