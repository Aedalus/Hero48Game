using UnityEngine;
using System.Collections;

public class KeyInput : MonoBehaviour {

    /*
     * Used to send the arrow key input to the cellhandler
     */
	// Use this for initialization
    CellHandler cellHandler;
	void Start () {
        cellHandler = (CellHandler)GameObject.Find("Cells").GetComponent("CellHandler");
	}
	
	// Update is called once per frame
	void Update () {
        
        //UpArrow
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            cellHandler.Swipe(Direction.UP);
        }

        //DownArrow
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            cellHandler.Swipe(Direction.DOWN);
        }

        //LeftArrow
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            cellHandler.Swipe(Direction.LEFT);
        }

        //RightArrow
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            cellHandler.Swipe(Direction.RIGHT);
        }
	}
}
