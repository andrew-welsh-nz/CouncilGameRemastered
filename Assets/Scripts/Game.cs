using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    // The text to alert the player the game is over
    [SerializeField]
    Text gameoverText;

    // Bools to check if it is time to return to the menu
    bool inGame;
    bool gameOverTimerStarted = false;

    // The score of the player
    float score;

    // The time passed since the game began
    float TimePassed;

    // Timer to delay returning to the menu for a short time
    float gameOverTimer;

    // Timers can go here

	// Use this for initialization
	void Start () {
        inGame = true;
	}
	
	// Update is called once per frame
	void Update () {
        TimePassed += Time.deltaTime;
        score = (int)TimePassed;

        if(!inGame && !gameOverTimerStarted)
        {
            gameOverTimerStarted = true;
        }
        else if(!inGame && gameOverTimerStarted)
        {
            gameOverTimer += Time.deltaTime;
        }

        if(gameOverTimer >= 5.0f)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver(int _reason)
    {
        if(inGame)
        {
            // The game is over, and the end UI should be shown
            Debug.Log("GAME OVER");
            Debug.Log("Score: " + score.ToString());

            gameoverText.gameObject.SetActive(true);

            inGame = false;

            switch (_reason)
            {
                case 0:
                    // Change the text to say the baby left
                    gameoverText.text = "baby left";
                    break;
                case 1:
                    // Change the text to say the dog messed up the sofa
                    gameoverText.text = "sofa ruined";
                    break;
                case 2:
                    //Tree has fallen on the house
                    gameoverText.text = "tree fell";
                    break;
                default:
                    break;
            }
        }
    }
}
