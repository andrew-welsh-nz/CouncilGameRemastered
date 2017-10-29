using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedyEntity : MonoBehaviour {

    [SerializeField]
    float waitTime;

    bool needing;

    Baby babyEntity = null;
    Dog dogEntity = null;

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
        // If the item collides with the owner of the store, set it to be held and ignore any further collisions with the player
        if (_col.gameObject.tag == "NeededObject" && needing == true)
        {
            if(babyEntity != false)
            {
                babyEntity.occupied = true;
            }
            else if (dogEntity != false)
            {
                dogEntity.occupied = true;
            }
        }
    }

    // Timer here
    IEnumerator WaitAndSet()
    {
        yield return new WaitForSeconds(waitTime);
        if (babyEntity != false)
        {
            babyEntity.occupied = false;
        }
        else if (dogEntity != false)
        {
            dogEntity.occupied = false;
        }
    }
}
