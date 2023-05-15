using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ShapeMovement : MonoBehaviour
{
    bool isDrag = false;

    GameObject touchedShape;
    Vector3 touchedShapePos;
    Touch touch;

    void Update()
    {
        PlayerController();
    }

    void PlayerController()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                TouchShape();
            }

            if (touch.phase == TouchPhase.Moved)
            {
                DragShape();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                DropShape();
            }

            
        }
    }

    void TouchShape()
    {
        RaycastHit2D hit = Physics2D.Raycast
            (Camera.main.ScreenToWorldPoint(touch.position), 
            Vector2.zero);

        if(hit.collider != null && hit.collider.tag == "Shape")
        {
            isDrag = true;
            touchedShape = hit.collider.gameObject;
            touchedShapePos = touchedShape.transform.position;
        }
    }

    void DragShape()
    {
        if (isDrag)
        {
            touchedShapePos = Camera.main.ScreenToWorldPoint(touch.position);
            touchedShapePos.z = 0;
            touchedShape.transform.position = touchedShapePos;
        }
    }

    void DropShape()
    {
        isDrag = false;
    }
}
