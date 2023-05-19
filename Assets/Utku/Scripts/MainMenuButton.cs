using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void QuitApplication()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Debug.Log("LevelYüklendi");
    }
}
