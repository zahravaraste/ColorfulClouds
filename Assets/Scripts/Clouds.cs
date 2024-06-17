using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clouds : MonoBehaviour
{
    public TMP_Text colorText;
    public TMP_Text scoreText;
    public SpriteRenderer topCloud;
    public SpriteRenderer bottomCloud;
    public SpriteRenderer leftCloud;
    public SpriteRenderer rightCloud;
    public SpriteRenderer [] spriteRenderer {get; private set;}
    
    public SpriteRenderer [] clouds;
    public List<string> usedColors = new List<string>();
    public string[] colors = new string[] {"Red", "Green", "Blue", "Yellow", "Pink", "Purple", "Orange"};
    public int currentColorIndex;
    public string currentColor;
    public SpriteRenderer selectedCloud;
    public Sprite[] sprites = new Sprite[7];
    public int score = 0;

    public AudioSource correctAudio, wrongAudio, backgroundMusic;

    public float timeRemaining = 20;
    public TMP_Text timerText;
    public bool timerIsRunning = false;
    
    private void Start()
    {
        timerIsRunning = true;
        clouds = new SpriteRenderer[] { topCloud, bottomCloud, leftCloud, rightCloud };
        backgroundMusic.Play();
        Invoke(nameof(SetRandomColor),2f);
    }

    private void SetRandomColor()
    {
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer item in spriteRenderer)
        {
            usedColors.Add(item.sprite.name);
        }

        do
        {
            currentColorIndex = Random.Range(0, colors.Length);
            currentColor = colors[currentColorIndex];
        } while (usedColors.Contains(currentColor));

        colorText.text = currentColor.ToString();
        usedColors.Clear();

        selectedCloud = clouds[Random.Range(0, clouds.Length)];
        selectedCloud.sprite = sprites[currentColorIndex];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CheckSelection(topCloud);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CheckSelection(bottomCloud);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CheckSelection(leftCloud);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CheckSelection(rightCloud);
        }

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                backgroundMusic.Stop();
            }
        }
    }

    private void CheckSelection(SpriteRenderer cloud)
    {
        if (cloud == selectedCloud)
        {
            correctAudio.Play();
            score += 5;
            scoreText.text = "Score: " + score;
            SetRandomColor();
        }
        else
        {
            wrongAudio.Play();
            SetRandomColor();
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
