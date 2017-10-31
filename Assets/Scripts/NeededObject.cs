using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeededObject : MonoBehaviour {

    [SerializeField]
    PlayerController player;

    bool isBeingHeld = false;

    bool collisionReset;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
