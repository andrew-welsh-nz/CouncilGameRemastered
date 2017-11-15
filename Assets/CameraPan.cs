using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour {

    private List<Vector3> PositionsToPanToo = new List<Vector3>();
    bool IsPanning = false;
    bool Continue = false;
    bool IsFinalPosition = false;

    private Vector3 OriginalPosition;
    public Vector3 OriginalOffset;

    private Vector3 PositionToPanToo;

    private float TimeCounter = 0.0f;

    [SerializeField]
    Game MainGame;

	// Use this for initialization
	void Start () {
        PositionToPanToo = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (IsPanning == false && PositionsToPanToo.Count > 0) {
            OriginalPosition = this.transform.position;
            OriginalOffset = this.GetComponent<CameraFollow>().CameraOffset;
            IsPanning = true;
            StartCoroutine(PanThroughPositions());
        }

        if (IsPanning == true && PositionToPanToo != new Vector3(0.0f, 0.0f, 0.0f)) {
            this.transform.position += ((PositionToPanToo + OriginalOffset * 0.5f) - this.transform.position) * (Time.deltaTime * 2.0f);
            TimeCounter += Time.deltaTime;
        }

        if (TimeCounter >= 1.25f) {
            TimeCounter = 0.0f;
            PositionToPanToo = new Vector3(0.0f, 0.0f, 0.0f);
            Continue = true;
        }
 
	}

    IEnumerator PanThroughPositions() {
        //Pause the game
        MainGame.IsPaused = true;

        foreach (Vector3 position in PositionsToPanToo) {
            PositionToPanToo = position;
            yield return new WaitUntil(() => Continue == true);
            Continue = false;
            if(IsFinalPosition)
                yield return new WaitForSeconds(5.0f);
            else
                yield return new WaitForSeconds(0.75f);
        }

        //return to original Position
        PositionToPanToo = OriginalPosition;
        yield return new WaitUntil(() => Continue == true);
        Continue = false;
        yield return new WaitForSeconds(1.0f);

        IsPanning = false;
        PositionsToPanToo.Clear();
        MainGame.IsPaused = false;
    }


    public void AddPositionToList(Vector3 _NewPosition)
    {
        PositionsToPanToo.Add(_NewPosition);
    }

    public void AddFinalPositionToList(Vector3 _NewPosition)
    {
        if (!IsFinalPosition)
        {
            PositionsToPanToo.Add(_NewPosition);
            IsFinalPosition = true;
        }
    }
}
