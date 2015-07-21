using UnityEngine;
using System;
using System.Collections;

public class CameraBounds : MonoBehaviour
{

    public float gameWidth;
    public float gameHeight;
    public void Awake()
    {
        GetComponent<Camera>().orthographicSize = 1f / GetComponent<Camera>().aspect * gameWidth / 2f;
        //getBoundsOfScreen();
    }


    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        float heightSize = gameHeight / 2;
        //t.h/t.w = c.h/c.w, find t.h, divide by 2 for orthographic size
        float widthSize = (1f / Camera.main.aspect * gameWidth)/2;

        Camera.main.orthographicSize = (widthSize > heightSize) ? widthSize : heightSize;

    }
}
