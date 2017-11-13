using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{

    [SerializeField]
    Game game;

    [SerializeField]
    SpeechBubble SpeechBubble;

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

    bool isAtSofa = false;
    float timeAtSofa;

    [SerializeField]
    float timeToDestroy;

    [SerializeField]
    ParticleSystem sofa;

    public bool occupied = false;

    Animator anim;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        agent.destination = target.position;
        sofa.Stop();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        anim.SetFloat("speed", agent.velocity.magnitude);
        anim.SetBool("held", isBeingHeld);

        Debug.Log("Speed:  " + agent.velocity.magnitude);

        // Move the item to the correct position if it is being held
        if (isBeingHeld)
        {
            this.transform.position = player.holdPosition.transform.position;
            this.transform.rotation = player.holdPosition.transform.rotation;
            SpeechBubble.gameObject.SetActive(false);
        }
        else
        {
            SpeechBubble.gameObject.SetActive(true);
            if (!occupied)
            {
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
                // The dog occupied by something and will not be moving towards the sofa
                SpeechBubble.gameObject.SetActive(false);
                agent.enabled = false;
            }
            

            //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - (1.0f * Time.deltaTime));

            if(isAtSofa)
            {
                timeAtSofa += Time.deltaTime;
            }

            if(timeAtSofa >= timeToDestroy)
            {
                game.GameOver(1);
            }
        }
    }

    void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.tag == "Sofa")
        {
            // The baby is outide and the game is over
            //Debug.Log("~~~~GAME OVER~~~~");

            isAtSofa = true;
            sofa.Play();
            anim.SetTrigger("interact");
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
        if(_col.gameObject.tag == "Sofa")
        {
            isAtSofa = false;
            timeAtSofa = 0.0f;
            sofa.Stop();
        }
    }

    public void Release()
    {
        Debug.Log("Dropping Dog");
        isBeingHeld = false;
        timeSinceRelease = 0.0f;
        agent.enabled = true;
        agent.SetDestination(target.position);
    }
}
