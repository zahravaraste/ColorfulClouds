using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

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
        Invoke(nameof(SetRandomColor),0.5f);
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

    private void SetRandomColorLevel2()
    {
        int [] arr = {0, 1, 2, 3, 4, 5, 6};
        int [] fourColorsIndex = GetRandomColors(arr);
        currentColorIndex = Random.Range(0, 4);
        currentColor = colors[fourColorsIndex[currentColorIndex]];
        colorText.text = currentColor.ToString();

        for (int j = 0; j < clouds.Length; j++) 
        {
            clouds[j].sprite = sprites[fourColorsIndex[j]];
            if (fourColorsIndex[currentColorIndex] == fourColorsIndex[j]) {
                selectedCloud = clouds[j];
            }
        }
    }

    int [] GetRandomColors(int[] fourColorsIndex)
    {
        System.Random random = new System.Random();
        List<int> availableValues = fourColorsIndex.ToList();
        int[] result = new int[4];

        for (int i = 0; i < 4; i++)
        {
            int index = random.Next(availableValues.Count);
            result[i] = availableValues[index];
            availableValues.RemoveAt(index);
        }

        return result;
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
                GameOver();
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
            CheckScore(score);
        }
        else
        {
            wrongAudio.Play();
            CheckScore(score);
        }
    }

    private void CheckScore(int Score)
    {
        if (Score >= 50) 
        {
                SetRandomColorLevel2();
        }
        else {
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

    private void GameOver()
    {
        backgroundMusic.Stop();
        string sceneName = "GameOverScene";
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetInt("PlayerScore", score);

    }
}
