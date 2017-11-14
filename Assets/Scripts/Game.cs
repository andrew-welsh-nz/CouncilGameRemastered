using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    // The text to alert the player the game is over
    [SerializeField]
    Text gameoverText;

    [SerializeField]
    Text ObjectsPlacedText;

    [SerializeField]
    Text PhoneCallsMadeText;
    
    [SerializeField]
    Text DistractionsCausedText;

    // Bools to check if it is time to return to the menu
    bool inGame;
    bool gameOverTimerStarted = false;

    // The score of the player
    public float score;
    public int ObjectsPlaced = 0;
    public int PhoneCallsMade = 0;
    public int DistractionsCaused = 0;

    private float TimeScore;

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
        if(!gameOverTimerStarted)
            score += Time.deltaTime;

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
            int DisplayScore = (int)score;
            // The game is over, and the end UI should be shown
            Debug.Log("GAME OVER !");
            Debug.Log("Score: " + DisplayScore.ToString());

            gameoverText.gameObject.SetActive(true);
            ObjectsPlacedText.gameObject.SetActive(true);
            ObjectsPlacedText.text = "Objects Placed: " + ObjectsPlaced.ToString();
            PhoneCallsMadeText.gameObject.SetActive(true);
            PhoneCallsMadeText.text = "Phone Calls Made: " + PhoneCallsMade.ToString();
            DistractionsCausedText.gameObject.SetActive(true);
            DistractionsCausedText.text = "Distractions Caused: " + DistractionsCaused.ToString();

            inGame = false;

            switch (_reason)
            {
                case 0:
                    // Change the text to say the baby left
                    gameoverText.text = "BABY LEFT !";
                    break;
                case 1:
                    // Change the text to say the dog messed up the sofa
                    gameoverText.text = "SOFA RUINED !";
                    break;
                case 2:
                    //Tree has fallen on the house
                    gameoverText.text = "TREE FELL !";
                    break;
                case 3:
                    //The player has won the game
                    gameoverText.text = "YOU WIN !";
                    break;
                default:
                    break;
            }
        }
    }
}
