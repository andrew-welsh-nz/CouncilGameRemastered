using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWritter : MonoBehaviour {

    public float TextDelay;
    public string fullText;
    private string currentText = "";
    public bool IsTextGood;
    public GameObject Phone;

	// Use this for initialization
	void Start () {
        
	}

    public void TypeText() {
        this.GetComponent<Text>().text = "";
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText() {
        for (int i = 1; i < fullText.Length + 1; i++) {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;

            if (IsTextGood == true){
                if (i % 5 == 0)
                Phone.GetComponent<clickPlayer>().PlaySound();
            }
            else {
                if (i % 3 == 0)
                    Phone.GetComponent<clickPlayer>().PlaySound();
            }

            yield return new WaitForSeconds(TextDelay);
        }

        //After Text Finished Tpying, allow the Player to continue
        Phone.GetComponent<Phone>().IsTextFinished = true;
    }	


}
