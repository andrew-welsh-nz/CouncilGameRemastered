using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickPlayer : MonoBehaviour {

    AudioSource audioSound;

    [SerializeField]
    float lowerRange;

    [SerializeField]
    float upperRange;

	// Use this for initialization
	void Start () {
        audioSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySound()
    {
        audioSound.pitch = Random.Range(lowerRange, upperRange);

        audioSound.Play();
    }
}
