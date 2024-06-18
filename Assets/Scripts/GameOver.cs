using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TMP_Text scoreText;
    public int score;

    private void Start()
    {
        score = PlayerPrefs.GetInt("PlayerScore");
        scoreText.text = "Score: " + score;
    }
}
