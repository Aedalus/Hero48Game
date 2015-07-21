using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    Vector3 display;
    Vector3 home;

    float speed = 10;
	// Use this for initialization
	void Start () {
        home = transform.position;
        display = transform.position + Vector3.up * 3;
	}
	
	// Update is called once per frame
	void Update () {
        if (HighScores.active == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, display, Time.deltaTime * speed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, home, Time.deltaTime * speed);
        }
	}
}
