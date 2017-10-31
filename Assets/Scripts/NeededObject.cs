using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeededObject : MonoBehaviour {

    [SerializeField]
    PlayerController player;

    bool isBeingHeld = false;

    bool collisionReset;

    float timeSinceRelease = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isBeingHeld)
        {
            this.transform.position = player.holdPosition.transform.position;
            this.transform.rotation = player.holdPosition.transform.rotation;
        }
        else
        {
            if (timeSinceRelease >= 2.5f && !collisionReset)
            {
                Debug.Log("Allowing collisions again");
                Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), false);
                collisionReset = true;
            }
            else
            {
                timeSinceRelease += Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter(Collision _col)
    {
        // If the item collides with the owner of the store, set it to be held and ignore any further collisions with the player
        if (_col.gameObject.tag == "Player" && !player.isHolding)
        {
            isBeingHeld = true;
            player.isHolding = true;
            Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), true);
            collisionReset = false;
            player.holdingItem = this.gameObject;
        }
    }
    public void Release()
    {
        Debug.Log("Dropping baby");
        isBeingHeld = false;
        timeSinceRelease = 0.0f;
    }
}
