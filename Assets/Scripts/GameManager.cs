using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 
using UnityEngine.SceneManagement;
using UnityEngine.UI;                   //Allows us to use Text
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    public bool showDebugUI;

    const float roundTime = 180;                            //Total seconds for a round  
    private int score;                                      //Current score (of the day)
    private float startTimestamp;                           //Timestamp for when the day started
    private float timeLeft;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        startTimestamp = Time.time;
    }

    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            timeLeft = roundTime - (Time.time - startTimestamp);
        else
            timeLeft = -1;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
                Application.Quit();
            else
            {
                LoadScene(0);
            }
        }

        if (SceneManager.GetActiveScene().name != "main menu")
        {
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                // TODO End round, show score and go back to main menu
                SetHighScore(); 
            }
        }
        
    }

    void SetHighScore()
    {
        if (SceneManager.GetActiveScene().name == "unloading day")
        {
            int hs = PlayerPrefs.GetInt("unloadinghighscore", defaultValue: 0);
            if (hs < score)
            {
                PlayerPrefs.SetInt("unloadinghighscore", score);
            }
        }
        else if (SceneManager.GetActiveScene().name == "sorting day")
        {
            int hs = PlayerPrefs.GetInt("sortinghighscore", defaultValue: 0);
            if (hs < score)
            {
                PlayerPrefs.SetInt("sortinghighscore", score);
            }
        }
        else if (SceneManager.GetActiveScene().name == "day 3")
        {
            int hs = PlayerPrefs.GetInt("day3highscore", defaultValue: 0);
            if (hs < score)
            {
                PlayerPrefs.SetInt("day3highscore", score);
            }
        }
    }

    void OnGUI()
    {
       if(showDebugUI)
        {
            GUILayout.Label("Scene: " + SceneManager.GetActiveScene().name);
            GUILayout.Label("Time Left: " + GetTimeLeft());
            GUILayout.Label("Score: " + GetScore());
        }

        if (SceneManager.GetActiveScene().name == "main menu")
        {
            Text uhs = GameObject.Find("high score unloading").GetComponent<Text>();
            uhs.text = "HIGH SCORE: " + PlayerPrefs.GetInt("unloadinghighscore", defaultValue: 0);
            Text shs = GameObject.Find("high score sorting").GetComponent<Text>();
            shs.text = "HIGH SCORE: " + PlayerPrefs.GetInt("sortinghighscore", defaultValue: 0);
            Text dhs = GameObject.Find("high score day 3").GetComponent<Text>();
            dhs.text = "HIGH SCORE: " + PlayerPrefs.GetInt("day3highscore", defaultValue: 0);
        }
    }

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i, LoadSceneMode.Single);
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
}