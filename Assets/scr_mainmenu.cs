using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_mainmenu : MonoBehaviour {

    public GameObject Camera;
    Animator anim;

    bool started = false;

    GameObject logo;
    Animator logoAnim;

    float playTimer = 0;
    float playTimerReset = 3;

    bool playTriggered = false;

    float quitTimer = 0;
    float quitTimerReset = 1;
    bool quitTimerSet = false;

    // Use this for initialization
    void Start () {
        anim = Camera.GetComponent<Animator>();

        logo = transform.Find("Logo").gameObject;
        logoAnim = logo.GetComponent<Animator>();

        logo.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {

        if (started)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                Menu();

            if (Input.GetKeyDown(KeyCode.Alpha2))
                Settings();

            if (Input.GetKeyDown(KeyCode.Alpha3))
                Credits();

            if (Input.GetKeyDown(KeyCode.Alpha4))
                Play();

            if (Input.GetKeyDown(KeyCode.Alpha5))
                Quit();
        }
        else
        {
            if(Input.anyKeyDown)
            {
                started = true;
                Debug.Log("Anykey");
                StartTrigger();
            }
        }

        if (playTriggered)
        {
            if (playTimer > 0)
            {
                playTimer -= 1 * Time.deltaTime;
            }

            if (playTimer <= 0)
            {
                startGame();
            }
        }

        if (quitTimerSet)
        {
            if (quitTimer > 0)
            {
                quitTimer -= 1 * Time.deltaTime;
            }

            if (quitTimer <= 0)
            {
                Application.Quit();
            }
        }
    }

    public void StartTrigger()
    {
        anim.SetTrigger("start");
        Debug.Log("Started");

        logoAnim.SetTrigger("start");
    }

    public void Settings()
    {
        anim.SetTrigger("settings");
        Debug.Log("Settings");
    }

    public void Credits()
    {
        anim.SetTrigger("credits");
        Debug.Log("Credits");
    }
    public void Menu()
    {
        anim.SetTrigger("menu");
        Debug.Log("Menu");
    }

    public void Play()
    {
        anim.SetTrigger("play");
        Debug.Log("Play");
        playTimer = playTimerReset;
        playTriggered = true;
    }

    public void Quit()
    {
        anim.SetTrigger("quit");
        Debug.Log("Quit");

        quitTimer = quitTimerReset;
        quitTimerSet = true;
    }

    public void startGame()
    {
        SceneManager.LoadScene("main");
    }
}
