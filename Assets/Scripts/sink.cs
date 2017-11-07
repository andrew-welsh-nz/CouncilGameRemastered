using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sink : MonoBehaviour
{

    private bool StartFalling = false;
    private bool ResetTree = false;
    public float TimeToFall;
    public float TimeRemaining;
    private float ResetRotationAmount;
    private float ZPositionWhenCalled;
    private Color DefaultColor;
    private Color RedColor = new Color(1.0f, 0.1f, 0.1f);
    private float FlashCooldown;
    private Quaternion OriginalRotation;

    // Use this for initialization
    void Start()
    {
        if (Application.loadedLevelName == "main")
        {
            StartFalling = true;
            TimeRemaining = TimeToFall;
            DefaultColor = this.GetComponentInChildren<MeshRenderer>().material.color;
            OriginalRotation = transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {

        FlashCooldown += Time.deltaTime;

        if (TimeRemaining / TimeToFall <= 0.5f && FlashCooldown >= 0.25f && TimeRemaining > 1.0f)
        {
            if (this.GetComponentInChildren<MeshRenderer>().material.color != RedColor)
            {
                this.GetComponentInChildren<MeshRenderer>().material.color = RedColor;
            }
            else
            {
                this.GetComponentInChildren<MeshRenderer>().material.color = DefaultColor;
            }
            FlashCooldown = 0.0f;
        }

        //3 Seconds left, start falling
        if (TimeRemaining <= 1.0f)
        {
            this.GetComponentInChildren<MeshRenderer>().material.color = RedColor;
        }

        if (TimeRemaining <= 0.0f)
        {
            //Sink Flooded, add flooding code here
            ResetTree = false;
            StartFalling = false;
        }

        if (ResetTree == true)
        {
            TimeRemaining = TimeToFall;
            ResetTree = false;
            StartFalling = true;
            this.GetComponentInChildren<MeshRenderer>().material.color = DefaultColor;
            transform.rotation = Quaternion.Slerp(transform.rotation, OriginalRotation, Time.time * 0.5f);
        }
        else if (StartFalling == true)
        {
            TimeRemaining -= Time.deltaTime;
        }

    }

    public float GetTimeRemaining()
    {
        return TimeRemaining;
    }

    public void ResetHazard(bool _isCallGood)
    {
        StartFalling = false;
        ResetTree = true;
    }

}
