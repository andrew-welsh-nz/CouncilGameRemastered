using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    // The string that is attached to the end of the controls
    [SerializeField]
    string playerNumber;

    // The movement speed of the player
    [SerializeField]
    float moveSpeed;

    // The amount that the dash should scale regular movement by
    [SerializeField]
    float dashScale;

    //The interaction object belonging to the player
    [SerializeField]
    Interact interaction;

    // The deadzone on the controller, in which input will be ignored
    [SerializeField]
    float deadzone;

    [SerializeField]
    Game MainGame;

    // The position where held items will be placed
    public GameObject holdPosition;

    // Whether the player is holding an item or not
    public bool isHolding;

    public GameObject holdingItem;

    // To check if player is moving or not
    public bool isMoving;

    // The rigidbody that is attached to the player
    Rigidbody rb;

    // The animator that is attached to the player
    Animator anim;

    // The animator that is attached to the player's hair
    [SerializeField]
    Animator[] modelAnimators;

    // The current rotation that the player is facing
    Quaternion rot;

    [SerializeField]
    Interactable[] interactables;

    float OriginalMoveSpeed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = transform.Find("char_player").GetComponent<Animator>();
        OriginalMoveSpeed = moveSpeed;

    }
	
	// Update is called once per frame
	void Update () {
        if (MainGame.IsPaused)
        {
            moveSpeed = 0.0f;
        }
        else
        {
            moveSpeed = OriginalMoveSpeed;
            // Check whether the stick is outside of the deadzone. When using a keyboard it will always be over this
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");


            Vector3 inputControler = new Vector3(h, 0, v);
            Vector3 inputKeyboard = new Vector3(Input.GetAxis("Horizontal" + playerNumber), 0, Input.GetAxis("Vertical" + playerNumber));
            Vector3 input = new Vector3(0, 0, 0);

            // Uses either Keyboard input or controller input, Which ever is larger
            if (inputControler.magnitude > inputKeyboard.magnitude)
                input = inputControler;
            else
                input = inputKeyboard;

            // If the input is within the deadzone, ignore it. Also set the isMoving variable
            if (input.magnitude < deadzone)
            {
                input = Vector3.zero;
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }

            // Movement

            // Sets the velocity of the rigidbody
            rb.velocity = (input * moveSpeed);

            // Animation - choose whether to play the idle or running animation, then set the playback speed to the player's movement speed
            //anim.SetBool("isMoving", isMoving);
            anim.SetBool("holding", isHolding);
            anim.SetFloat("speed", (input.magnitude * 2));

            /*
            for(int i = 0; i < modelAnimators.Length; i++)
            {

                modelAnimators[i].SetBool("isMoving", isMoving);
                modelAnimators[i].SetBool("holding", isHolding);
                modelAnimators[i].SetFloat("speed", (input.magnitude * 2));

            }
            */

            // Player Rotation
            if (input != Vector3.zero)
            {
                rot = Quaternion.LookRotation(input);

                transform.rotation = rot;
            }

            if (CrossPlatformInputManager.GetButtonDown("Release" + playerNumber))
            {
                interaction.gameObject.SetActive(true);
            }

            if (CrossPlatformInputManager.GetButtonUp("Release" + playerNumber))
            {
                interaction.gameObject.SetActive(false);
                for (int i = 0; i < interactables.Length; i++)
                {
                    interactables[i].isInteracting = false;
                }
            }

            // Release Input
            if (CrossPlatformInputManager.GetButtonDown("Release" + playerNumber))
            {
                //interaction.gameObject.SetActive(true);
                //anim.SetTrigger("attack");
                //for(int i = 0; i < modelAnimators.Length; i++)
                //{
                //modelAnimators[i].SetTrigger("attack");
                //}

                Debug.Log("'X' Pressed");

                if (isHolding)
                {
                    if (holdingItem.tag == "Baby")
                    {
                        isHolding = false;
                        holdingItem.GetComponent<Baby>().Release();
                        holdingItem = null;
                        MainGame.ObjectsPlaced++;
                    }
                    else if (holdingItem.tag == "Dog")
                    {
                        isHolding = false;
                        holdingItem.GetComponent<Dog>().Release();
                        holdingItem = null;
                        MainGame.ObjectsPlaced++;
                    }
                    else if (holdingItem.tag == "NeededObject")
                    {
                        isHolding = false;
                        holdingItem.GetComponent<NeededObject>().Release();
                        holdingItem = null;
                        MainGame.ObjectsPlaced++;
                    }
                }
                else
                {
                    // Activate the interact item
                    interaction.gameObject.SetActive(true);
                    //interaction.timeSinceActive = 0.0f;


                    anim_interact();
                }
            }

            // Dash Input
            //if(Input.GetButtonDown("Dash" + playerNumber))
            //{
            //rb.AddForce(input * dashScale * moveSpeed, ForceMode.Impulse);
            //}
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water")
        {
            moveSpeed *= 0.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Water")
        {
            moveSpeed *= 2;
        }
    }

    public void anim_interact()
    {
        anim.SetTrigger("interact");
    }
}
