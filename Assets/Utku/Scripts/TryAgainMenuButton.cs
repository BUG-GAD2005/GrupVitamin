using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainMenuButton : MonoBehaviour
{
    
    public void StartNewgGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
  
}
