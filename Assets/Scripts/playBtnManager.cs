using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playBtnManager : MonoBehaviour
{
    public void PlayGame()
    {
        string sceneName = "MainScene"; 
        SceneManager.LoadScene(sceneName);
    }
}
