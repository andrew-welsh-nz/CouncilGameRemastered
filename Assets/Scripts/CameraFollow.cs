using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraFollow : MonoBehaviour {

    private Vector3 CameraOffset;
    private Vector3 basePosition;
    public Transform ObjectToFollow;

    [SerializeField]
    float ControllerOffsetScale = 1.0f;

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
        if (basePosition.x <= -11.0f)
        {
            basePosition.x = -11.0f;
        }
        else if (basePosition.x >= 5.5f) {
            basePosition.x = 5.5f;
        }

        //Checks if BasePosition is within the cameraFollow Bounds in the z Axis
        if (basePosition.z <= -1.5f) {
            basePosition.z = -1.5f;
        }
        else if (basePosition.z >= 12.0f) {
            basePosition.z = 12.0f;
        }

        var JoyX = CrossPlatformInputManager.GetAxis("RightJoystickX");
        var JoyY = CrossPlatformInputManager.GetAxis("RightJoystickY");

        Vector3 TargetPosition = basePosition + CameraOffset;

        Vector3 ControllerOffest = new Vector3(JoyX, 0, -JoyY);
        ControllerOffest = ControllerOffest.normalized;
        ControllerOffest *= ControllerOffsetScale;


        if (IsTweeningOn)
        {
            this.transform.position += (TargetPosition - this.transform.position) * TweeningValue;

            Vector3 NewTargetPos = (this.transform.position + ControllerOffest);
            Vector3 Velocity = (NewTargetPos - this.transform.position) / 2.0f;
            this.transform.position += Velocity * Time.deltaTime;
            //this.transform.position = Vector3.Lerp(this.transform.position, NewTargetPos, 0.1f);

        }
        else {
            this.transform.position = TargetPosition;
        }


	}
}
