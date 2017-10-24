using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour
{

    public Game game;
    private bool StartFalling = false;
    public float TimeToFall;
    private float TimeRemaining = 60.0f;


    // Use this for initialization
    void Start()
    {
        if (Application.loadedLevelName == "main")
        {
            StartFalling = true;
           // TimeRemaining = TimeToFall;
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    public float GetTimeRemaining()
    {
        return TimeRemaining;
    }

}
