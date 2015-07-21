using UnityEngine;
using System.Collections;

public class ExitKey : MonoBehaviour {

	/*
     * Hitting the back button (esc on PC) will save and bring up the tutorial
     */
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Save.SaveData();
            Application.LoadLevel("Tutorial");
        }
	}
}
