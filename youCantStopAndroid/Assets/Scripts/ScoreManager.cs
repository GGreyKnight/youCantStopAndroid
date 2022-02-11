using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;

    Car car;

    float temp = 0;
    int score = 0;
    int highScore = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        car = FindObjectOfType<Car>();
    }

    void Update()
    {
        if(car.hasCollide == false)
        {
            if(car.Speed() > 25f)
            {
                temp += (car.Speed() / 10) * Time.deltaTime;
            }

            if (car.Speed()>50)
            {
                temp += (car.transform.position.z / 100) * Time.deltaTime;
            }

            score = (int)temp;
            scoreText.text = "Score: " + score + " ";
        }
        
    }

    public void UpdateHighScore()
    {
        if(score>PlayerPrefs.GetInt("High score"))
        {
            highScore = score;
            PlayerPrefs.SetInt("High score", highScore);
        }
    }
}
