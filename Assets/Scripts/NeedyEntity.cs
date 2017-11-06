using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedyEntity : MonoBehaviour {

    [SerializeField]
    float waitTime;

    [SerializeField]
    float ThrownStrength;

    bool needing;

    Baby babyEntity = null;
    Dog dogEntity = null;

    GameObject CurrentNeededObject = null;

	// Use this for initialization
	void Start () {
		if(this.gameObject.tag == "Baby")
        {
            babyEntity = GetComponent<Baby>();
        }
        else if(this.gameObject.tag == "Dog")
        {
            dogEntity = GetComponent<Dog>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision _col)
    {
        Debug.Log("COLLISION");

        // If the item collides with the owner of the store, set it to be held and ignore any further collisions with the player
        if (_col.gameObject.tag == "NeededObject") //&& needing == true)
        {
            Debug.Log("Pausing");

            if (babyEntity != false)
            {
                CurrentNeededObject = _col.gameObject;
                babyEntity.occupied = true;
                needing = false;
                StartCoroutine("WaitAndSet");
            }
            else if (dogEntity != false)
            {
                CurrentNeededObject = _col.gameObject;
                dogEntity.occupied = true;
                needing = false;
                StartCoroutine("WaitAndSet");
            }
        }
    }

    // Timer here
    IEnumerator WaitAndSet()
    {
        Debug.Log("WaitandSet called");
        yield return new WaitForSeconds(waitTime);

        //Throw Object
        Debug.Log("Needy Object Thrown");
        CurrentNeededObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-5.0f, 5.0f), 1.0f, Random.Range(-5.0f, 5.0f)) * ThrownStrength);

        yield return new WaitForSeconds(0.2f);

        if (babyEntity != false)
        {
            CurrentNeededObject = null;
            babyEntity.occupied = false;
            needing = true;
        }
        else if (dogEntity != false)
        {
            CurrentNeededObject = null;
            dogEntity.occupied = false;
            needing = true;
        }
    }
}
