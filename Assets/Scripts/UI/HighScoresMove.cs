using UnityEngine;
using System.Collections;

public class HighScoresMove : MonoBehaviour {
    Vector3 display;
    Vector3 away;

    float speed = 10;
	// Use this for initialization
	void Awake () {
        display = transform.position;
        away = transform.position + Vector3.up * 2;
        transform.position = away;
	}
	
	// Update is called once per frame
	void Update () {
        if (HighScores.active == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, display, Time.deltaTime * speed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, away, Time.deltaTime * speed);
        }
	}
}
