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
        if (_col.gameObject.tag == "NeededObject" && _col.gameObject.GetComponent<NeededObject>().GetCanBeUsed()) //&& needing == true)
        {
            Debug.Log("Pausing");

            //Checks if the script is on a baby and then if the needy object is for the baby
            if (babyEntity != false && _col.gameObject.GetComponent<NeededObject>().CanOccupyBaby == true)
            {
                CurrentNeededObject = _col.gameObject;
                babyEntity.occupied = true;
                needing = false;
                StartCoroutine("WaitAndSet");
            }
            //Checks if the script is on a Dog and then if the needy object is for the Dog
            else if (dogEntity != false && _col.gameObject.GetComponent<NeededObject>().CanOccupyDog == true)
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

        //50% change the number is negative (this way we can exclude numbers -2.9f to 2.9f as they are too small)
        float ThrowXVelocity = Random.Range(3.0f, 5.0f);
        if (Random.Range(0.0f, 1.0f) < 0.5f)
            ThrowXVelocity *= -1.0f;

        float ThrowZVelocity = Random.Range(3.0f, 5.0f);
        if (Random.Range(0.0f, 1.0f) < 0.5f)
            ThrowZVelocity *= -1.0f;

        CurrentNeededObject.GetComponent<Rigidbody>().AddForce(new Vector3(ThrowXVelocity, 1.5f, ThrowZVelocity) * ThrownStrength);

        CurrentNeededObject.GetComponent<NeededObject>().SetCanBeUsed(false);

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
