using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnsValue : MonoBehaviour {

    int oldTurns;
    Text UIText;
	// Use this for initialization
	void Start () {
        UIText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (oldTurns != CellHandler.turns)
        {
            oldTurns = CellHandler.turns;
            UIText.text = CellHandler.turns.ToString();
        }
	
	}
}
