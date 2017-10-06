using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 
using UnityEngine.SceneManagement;
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

    void OnGUI()
    {
       if(showDebugUI)
        {
            GUILayout.Label("Scene: " + SceneManager.GetActiveScene().name);
            GUILayout.Label("Time Left: " + GetTimeLeft());
            GUILayout.Label("Score: " + GetScore());
        }
    }

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
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