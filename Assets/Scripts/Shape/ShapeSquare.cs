using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeSquare : MonoBehaviour
{
    [SerializeField] float checkingSize = 0.1f;

    public bool CheckForGridSquareToOccupy(out GridSquareScript outGridSquareScript)
    {
        outGridSquareScript = null;

        Collider2D[] collidedGridSquares = Physics2D.OverlapBoxAll(transform.position, Vector2.one * checkingSize, 0);
        GameObject closestGSquare = null;
        foreach (Collider2D col in collidedGridSquares)
        {
            if (col.gameObject.tag != "GridSquare") continue;

            float newDistance = Vector2.Distance(col.gameObject.transform.position, transform.position);

            if (closestGSquare == null || Vector2.Distance(closestGSquare.transform.position, transform.position) > newDistance)
            {
                closestGSquare = col.gameObject;
            }
        }

        if (closestGSquare != null)
        {
            if (closestGSquare.TryGetComponent(out GridSquareScript gss))
            {
                if (gss.isOccupied == false)
                {
                    outGridSquareScript = gss;
                    return true;
                }
            }
        }
        
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector2.one * checkingSize);
    }
}
