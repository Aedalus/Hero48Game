using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RestartSafety : MonoBehaviour {

    bool safetyCheck = false;
    Text buttonText;
    float lastClick;
    private float delay = 3f;
    CellHandler cellHandler; 

	// Use this for initialization
	void Start () {
        buttonText = GetComponentInChildren<Text>();
        cellHandler = GameObject.Find("Cells").GetComponent<CellHandler>();

        if (buttonText == null)
            Debug.LogError("Button Text is NUll");
        else if (cellHandler == null)
            Debug.LogError("cellHandler is null");
	}
	
	// Update is called once per frame
	void Update () {
        if (safetyCheck == true && (Time.time > lastClick + delay))
        {
            safetyCheck = false;
            buttonText.text = "Restart";
        }
	}

    public void safetyReset(){
        if (safetyCheck == false)
        {
            safetyCheck = true;
            buttonText.text = "Sure?";
            lastClick = Time.time;
            //Debug.Log(Time.time);
        }
        else //safetyCheck is true
        {
            safetyCheck = false;
            buttonText.text = "Restart";
            cellHandler.Restart();
        }

    }
}
