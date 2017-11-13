using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public bool isInteracting = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
