using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType {
    Player = 0,
    Phone = 1,
    Needy = 2,
}


public enum BubbleImage {
    SinkFloodingImage = 0,
    TreeFallingImage = 1,
    PhoneRingingImage = 2,
    DogScratchImage = 3,
    DogScaredImage = 4,
    BabyDoorImage = 5,
    BabyBottleImage = 6,
}


public class SpeechBubble : MonoBehaviour {

    [SerializeField]
    GameObject MainCamera;

    [SerializeField]
    ObjectType BubbleType;

    [SerializeField]
    GameObject TargetObject;

    [SerializeField]
    GameObject ChildObject;

    [SerializeField]
    Material Bubble;

    [SerializeField]
    Material SinkFlooding;

    [SerializeField]
    Material TreeFalling;

    [SerializeField]
    Material PhoneRinging;

    [SerializeField]
    Material DogScratch;

    [SerializeField]
    Material DogScared;

    [SerializeField]
    Material BabyDoor;

    [SerializeField]
    Material BabyBottle;

    // Use this for initialization
    void Start () {
        this.transform.forward = -MainCamera.transform.forward;
        this.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = TargetObject.transform.position + new Vector3(1.2f, 1.3f, 0.0f);
    }

    //Set the sprite in it's child object to the given sprite
    public void SetSprite(BubbleImage _BubbleImage) {
        Material ImageSelected = SinkFlooding;

        //Switches on the passed in Enum for the image
        switch (_BubbleImage) {
            case BubbleImage.SinkFloodingImage:
                ImageSelected = SinkFlooding;
                break;
            case BubbleImage.TreeFallingImage:
                ImageSelected = TreeFalling;
                break;
            case BubbleImage.PhoneRingingImage:
                ImageSelected = PhoneRinging;
                break;
            case BubbleImage.DogScratchImage:
                ImageSelected = DogScratch;
                break;
            case BubbleImage.DogScaredImage:
                ImageSelected = DogScared;
                break;
            case BubbleImage.BabyDoorImage:
                ImageSelected = BabyDoor;
                break;
            case BubbleImage.BabyBottleImage:
                ImageSelected = BabyBottle;
                break;
            default:
                break;
        }

        //Changes the sprite inside the bubble
        ChildObject.GetComponent<Renderer>().material = ImageSelected;
    }

    //Return the Objects type e.g "Phone"
    ObjectType GetObjectType() {
        return BubbleType;
    }

}
