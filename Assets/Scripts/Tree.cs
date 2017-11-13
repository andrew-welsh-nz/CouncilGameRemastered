using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    private bool StartFalling = false;
    private bool ResetTree = false;
    public float TimeToFall;
    float TimeRemaining;
    private float ResetRotationAmount;
    private float ZPositionWhenCalled;
    private Color DefaultColor;
    private Color RedColor = new Color(1.0f, 0.1f, 0.1f);
    private float FlashCooldown;
    private Quaternion OriginalRotation;

    [SerializeField]
    SpeechBubble PhoneSpeechBubble;

    // Use this for initialization
    void Start () {
        if (Application.loadedLevelName == "main") {
            StartFalling = true;
            TimeRemaining = TimeToFall;
            DefaultColor = this.GetComponentInChildren<MeshRenderer>().material.color;
            OriginalRotation = transform.rotation;
        }
	}
	
	// Update is called once per frame
	void Update () {

        FlashCooldown += Time.deltaTime;

        if (TimeToFall > 18)
        {
            TimeToFall -= Time.deltaTime * 0.25f;
        }
        else {
            TimeToFall = 18;
        }

        if (TimeRemaining / TimeToFall <= 0.5f && FlashCooldown >= 0.25f && TimeRemaining > 1.0f) {
            PhoneSpeechBubble.gameObject.SetActive(true);
            if (this.GetComponentInChildren<MeshRenderer>().material.color != RedColor)
            {
                this.GetComponentInChildren<MeshRenderer>().material.color = RedColor;
                PhoneSpeechBubble.SetSprite(BubbleImage.TreeFallingImage);
                Debug.Log("Changed to Red");
            }
            else {
                this.GetComponentInChildren<MeshRenderer>().material.color = DefaultColor;
                PhoneSpeechBubble.SetSprite(BubbleImage.PhoneRingingImage);
                Debug.Log("Changed to Green");
            }
            FlashCooldown = 0.0f;
        }

        //3 Seconds left, start falling
        if (TimeRemaining <= 1.0f && TimeRemaining > 0.0f) {
            Debug.Log("Tree Falling");
            this.GetComponentInChildren<MeshRenderer>().material.color = RedColor;
            transform.Rotate(-75.0f / 1.0f * Time.deltaTime, 0.0f, 0.0f);
        }

        if (TimeRemaining <= 0.0f) {
            PhoneSpeechBubble.gameObject.SetActive(false);
            ResetTree = false;
            StartFalling = false;
        }

        if (ResetTree == true) {
            TimeRemaining = TimeToFall;
            PhoneSpeechBubble.gameObject.SetActive(false);
            PhoneSpeechBubble.SetSprite(BubbleImage.PhoneRingingImage);
            ResetTree = false;
            StartFalling = true;
            this.GetComponentInChildren<MeshRenderer>().material.color = DefaultColor;
            transform.rotation = Quaternion.Slerp(transform.rotation, OriginalRotation, Time.time * 0.5f);
        }
        else if (StartFalling == true) {
            TimeRemaining -= Time.deltaTime;
        }
          
	}

    public float GetTimeRemaining() {
        return TimeRemaining;
    }

    public void ResetHazard(bool _isCallGood) {
        StartFalling = false;
        ResetTree = true;
    }

}
