using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Vector3 CameraOffset;
    private Vector3 basePosition;
    public Transform ObjectToFollow;

    //Value between 0 - 1. 1 Being true following
    public float TweeningValue;

    //Used for Debugging
    public bool IsTweeningOn;

	// Use this for initialization
	void Start () {
        CameraOffset = this.transform.position - ObjectToFollow.position;
    }
	
	// Update is called once per frame
	void Update () {
        basePosition = ObjectToFollow.position;

        //Checks if BasePosition is within the cameraFollow Bounds in the x Axis
        if (basePosition.x <= -3.5f)
        {
            basePosition.x = -3.5f;
        }
        else if (basePosition.x >= 3.5f) {
            basePosition.x = 3.5f;
        }

        //Checks if BasePosition is within the cameraFollow Bounds in the z Axis
        if (basePosition.z <= -1.0f) {
            basePosition.z = -1.0f;
        }
        else if (basePosition.z >= 4.0f) {
            basePosition.z = 4.0f;
        }

        Vector3 TargetPosition = basePosition + CameraOffset;

        if (IsTweeningOn)
        {
            this.transform.position += (TargetPosition - this.transform.position) * TweeningValue;
        }
        else {
            this.transform.position = TargetPosition;
        }


	}
}
