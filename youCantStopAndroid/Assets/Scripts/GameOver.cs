using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject gameOverScreen;
    public Text points;
    public Text highScore;
    bool gameOver;

    void Start()
    {
        FindObjectOfType<Car>().OnPlayerDeath += OnGameOver; //subscribing method
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void OnGameOver()
    {
        points.text = "Your " + ScoreManager.instance.scoreText.text.ToLower();
        FindObjectOfType<ScoreManager>().UpdateHighScore();
        highScore.text = "High score: " + PlayerPrefs.GetInt("High score") + " ";

        gameOverScreen.SetActive(true);
        gameOver = true;
    }
}
