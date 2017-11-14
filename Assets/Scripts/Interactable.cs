using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    [SerializeField]
    Game game;

    public bool isInteracting = false;

    [SerializeField]
    float targetPoints = 0.0f;

    float currentPoints;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isInteracting)
        {
            currentPoints += Time.deltaTime;
        }

        if(currentPoints >= targetPoints)
        {
            game.GameOver(3);
        }
	}

    void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.tag == "Interact")
        {
            isInteracting = true;
            Debug.Log("Interaction");
        }
    }

    void OnTriggerExit(Collider _col)
    {
        if (_col.gameObject.tag == "Interact")
        {
            isInteracting = false;
        }
    }
}
