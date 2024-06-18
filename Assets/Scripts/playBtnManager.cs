using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playBtnManager : MonoBehaviour
{
    private static playBtnManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayGame()
    {
        string sceneName = "MainScene"; 
        SceneManager.LoadScene(sceneName);
    }
}
