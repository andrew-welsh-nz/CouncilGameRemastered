using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Font FontToUse;
    public float TextSize;

    [SerializeField]
    Game MainGame;

    private int Score;
    private float TimePassed;
    private RectTransform ThisTransform;

    // Use this for initialization
    void Start () {
        Score = 0;
        TimePassed = 0.0f;

        this.GetComponent<Text>().text = "Score: " + Score.ToString();
        this.GetComponent<Text>().font = FontToUse;
        this.GetComponent<Text>().fontSize = (int)TextSize;

    }
	
	// Update is called once per frame
	void Update () {
        int DisplayScore = (int)MainGame.score;
        this.GetComponent<Text>().text = "Score: " + DisplayScore.ToString();
	}
}
