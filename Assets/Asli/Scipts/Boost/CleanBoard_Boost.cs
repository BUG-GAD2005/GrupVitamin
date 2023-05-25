using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanBoard_Boost : MonoBehaviour
{
    public GridScript gridScript;

    private void Start()
    {
        gridScript = FindAnyObjectByType<GridScript>();
    }
    public void CleanBoard()
    {
        gridScript.UnoccupyAll();
        Destroy(gameObject);
    }
}
