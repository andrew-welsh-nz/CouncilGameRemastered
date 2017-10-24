using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

    public float timeSinceActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceActive += Time.deltaTime;

        if(timeSinceActive >= 0.5f)
        {
            timeSinceActive = 0.0f;
            this.gameObject.SetActive(false);
        }
	}
}
