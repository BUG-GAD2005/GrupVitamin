using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainMenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartNewgGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
  
}
