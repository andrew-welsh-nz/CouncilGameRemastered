using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour {

    Rigidbody rb;
    public GameObject TextDisplayWindow;
    public GameObject WaterPipeHazard;
    public GameObject TreeHazard;
    public GameObject DisplayIconObject;
    public Texture GoodIcon;
    public Texture BadIcon;
    List<GameObject> Hazards;
    List<string> GoodGreetings = new List<string>();
    List<string> GoodResponses = new List<string>();
    List<string> BadGreetings = new List<string>();
    List<string> BadResponses = new List<string>();

    private bool Continue = false;
    public bool IsTextFinished = false;
    private bool AlreadyInACall = false;

    private Color DefaultColor;
    private Color WhiteColor = new Color(0.1f, 1.0f, 0.1f);
    private float FlashCooldown;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

        DefaultColor = this.GetComponent<MeshRenderer>().material.color;

        //Load Hazards into list
        Hazards = new List<GameObject>();
        Hazards.Add(WaterPipeHazard);
        Hazards.Add(TreeHazard);


        GoodGreetings.Add("Thank you for contacting the Council, How can we help you today?");
        GoodResponses.Add("HAZARD, we'll send help right away! Have a good day");

        BadGreetings.Add("This is the Council, What problem can we help you with today?");
        BadResponses.Add("HAZARD, we'll inform who can help as soon as possible. Goodbye");

    }
	
	// Update is called once per frame
	void Update () {
        FlashCooldown += Time.deltaTime;

        if (IsTextFinished == true) {
                IsTextFinished = false;
                Continue = true;
         }

        if (TreeHazard.GetComponent<Tree>().GetTimeRemaining() / TreeHazard.GetComponent<Tree>().TimeToFall <= 0.5f && FlashCooldown >= 0.25f && TreeHazard.GetComponent<Tree>().GetTimeRemaining() > 0.0f) {
            FlashCooldown = 0.0f;
            if (this.GetComponentInChildren<MeshRenderer>().material.color != WhiteColor)
            {
                this.GetComponentInChildren<MeshRenderer>().material.color = WhiteColor;
            }
            else
            {
                this.GetComponentInChildren<MeshRenderer>().material.color = DefaultColor;
            }
        }

        if (TreeHazard.GetComponent<Tree>().GetTimeRemaining() / TreeHazard.GetComponent<Tree>().TimeToFall > 0.5f) {
            this.GetComponentInChildren<MeshRenderer>().material.color = DefaultColor;
        }
    }

    void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.tag == "Interact" && AlreadyInACall == false)
        {
            // The phone is being interacted with, make the call I guess?
            Debug.Log("Phone Interaction");
            AlreadyInACall = true;
            StartCoroutine(PhoneInteraction());
        }
    }

    IEnumerator PhoneInteraction() {
        //Layout/////////////////
        //  - Find which "hazard" is the closest to failing (add in later but use temp variables for now)
        //      -Read all "Hazard" objects and look at their timer variable to decide which one needs fixing
        //      -Add the name of it to a temp string to use in the phone dialog
        //  - Fix the hazard that needs fixing
        //      -Reverse the animation? also the speed of it is based on its time value (which now goes up)
        //      -Free PhoneIsInUse Bool after this action is complete.

        IsTextFinished = false;
        Continue = false;   
        GameObject ImmediateHazard;

        if (TreeHazard.GetComponent<Tree>().GetTimeRemaining() <= WaterPipeHazard.GetComponent<Well>().GetTimeRemaining())
        {
            ImmediateHazard = TreeHazard;
        }
        else {
            ImmediateHazard = WaterPipeHazard;
        }
        

        Text DisplayText = TextDisplayWindow.GetComponentInChildren<Text>();
        DisplayText.text = "";
        RawImage DisplayIcon = DisplayIconObject.GetComponent<RawImage>();
        TypeWritter TextTypeWritter = TextDisplayWindow.GetComponentInChildren<TypeWritter>();

        if (Random.Range(1, 100) >= 35)
        {
            //Good Call will happen
            Debug.Log("Good Call Occured");
            TextDisplayWindow.SetActive(true);
            TextDisplayWindow.GetComponent<RawImage>().color = new Color(1.0f, 1.0f, 1.0f);
            TextTypeWritter.IsTextGood = true;

            TextTypeWritter.fullText = GoodGreetings[Random.Range(0, GoodGreetings.Count)];
            TextTypeWritter.TypeText();
       
            DisplayIcon.texture = GoodIcon;

            //Waits until Continue == true
            yield return new WaitUntil(() => Continue == true); 
            Continue = false;

            yield return new WaitForSeconds(0.5f);

            string ResponseText = GoodResponses[Random.Range(0, GoodResponses.Count)];
            if (ResponseText.Contains("HAZARD"))
            {
                if (ImmediateHazard.name == "tree") {
                    ResponseText = ResponseText.Replace("HAZARD", "A falling tree?");
                }
                else {
                    ResponseText = ResponseText.Replace("HAZARD", "A pipe is about to burst?");
                }
            }

            TextTypeWritter.fullText = ResponseText;
            TextTypeWritter.TypeText();

            yield return new WaitUntil(() => Continue == true);
            Continue = false;

            yield return new WaitForSeconds(0.5f);

            //Dialog Finished, Reset Variables and Handle fixing the Hazard
            IsTextFinished = false;
            TextDisplayWindow.SetActive(false);

            if (ImmediateHazard.name == "tree") {
                Debug.Log("Reset Tree Called");
                ImmediateHazard.GetComponent<Tree>().ResetHazard(true);
            }

        }
        else {
            //Bad call will happen
            Debug.Log("Bad Call Occured");
            TextDisplayWindow.SetActive(true);
            TextDisplayWindow.GetComponent<RawImage>().color = new Color(0.8f, 0.2f, 0.2f);
            TextTypeWritter.IsTextGood = false;

            TextTypeWritter.TextDelay = TextTypeWritter.TextDelay * 2;

            TextTypeWritter.fullText = BadGreetings[Random.Range(0, BadGreetings.Count)];
            TextTypeWritter.TypeText();
            DisplayIcon.texture = BadIcon;

            //Waits until Continue == true
            yield return new WaitUntil(() => Continue == true);
            Continue = false;

            yield return new WaitForSeconds(1.0f);

            string ResponseText = BadResponses[Random.Range(0, BadResponses.Count)];
            if (ResponseText.Contains("HAZARD"))
            {
                if (ImmediateHazard.name == "tree")
                {
                    ResponseText = ResponseText.Replace("HAZARD", "A falling tree?");
                }
                else
                {
                    ResponseText = ResponseText.Replace("HAZARD", "A pipe is about to burst?");
                }
            }

            TextTypeWritter.fullText = ResponseText;
            TextTypeWritter.TypeText();

            yield return new WaitUntil(() => Continue == true);
            Continue = false;

            yield return new WaitForSeconds(1.0f);

            //Dialog Finished, Reset Variables and Handle fixing the Hazard
            IsTextFinished = false;
            TextDisplayWindow.SetActive(false);
            TextTypeWritter.TextDelay = TextTypeWritter.TextDelay / 2;

            if (ImmediateHazard.name == "tree")
            {
                Debug.Log("Reset Tree Called");
                ImmediateHazard.GetComponent<Tree>().ResetHazard(false);
            }
        }
        AlreadyInACall = false;
    }




}
