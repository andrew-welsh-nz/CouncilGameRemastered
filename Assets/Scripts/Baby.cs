using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Baby : MonoBehaviour {

    [SerializeField]
    Game game;

    [SerializeField]
    Transform target;

    // Whether the object is being held or not
    [SerializeField]
    bool isBeingHeld;

    // The store and position that the item belongs to
    [SerializeField]
    PlayerController player;

    // The rigidbody attached to the item
    Rigidbody rb;

    NavMeshAgent agent;

    float timeSinceRelease;
    bool collisionReset;

    public bool occupied = false;

    public Animator anim;
    public bool held = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        agent.destination = target.position;

        anim = transform.Find("char_baby").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the item to the correct position if it is being held
        if (isBeingHeld)
        {
            this.transform.position = player.holdPosition.transform.position;
            this.transform.rotation = player.holdPosition.transform.rotation;

            
        }
        else
        {
            if(!occupied)
            {
                // The baby doesn't have what it needs, and is going to run for the door
                if (!agent.enabled)
                {
                    agent.enabled = true;
                    agent.SetDestination(target.position);
                }
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
            else
            {
                // The baby has the needed object, and can stay occupied with it for a bit
                agent.enabled = false;
            }

            //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - (1.0f * Time.deltaTime));
        }

        AnimUpdate();
    }

    void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.tag == "Outside")
        {
            // The baby is outide and the game is over
            //Debug.Log("~~~~GAME OVER~~~~");

            game.GameOver(0);
        }
    }

    private void OnCollisionEnter(Collision _col)
    {
        // If the item collides with the owner of the store, set it to be held and ignore any further collisions with the player
        if (_col.gameObject.tag == "Player" && !player.isHolding)
        {
            agent.enabled = false;
            isBeingHeld = true;
            player.isHolding = true;
            Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), true);
            collisionReset = false;
            player.holdingItem = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider _col)
    {
        // If the item exits it's home, allow it to colldie with the player
        //if(_col.gameObject.tag == "Home")
        //{
        //    if (_col.GetComponent<Home>().item == this)
        //    {
        //        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        //        //homeStore.itemsAtHome--;
        //    }
        //}
    }

    public void Release()
    {
        Debug.Log("Dropping baby");
        isBeingHeld = false;
        timeSinceRelease = 0.0f;
        agent.enabled = true;
        agent.SetDestination(target.position);
    }

    public void AnimUpdate()
    {
        if (isBeingHeld && !held)
        {
            held = true;

            anim.SetBool("held", true);

            Debug.Log("Held");
        }

        if (!isBeingHeld && held)
        {
            held = false;

            anim.SetBool("held", false);
        }
        
        anim.SetFloat("speed", agent.velocity.magnitude);
    }
}
