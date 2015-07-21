using UnityEngine;
using System.Collections;

public class MouseInput : MonoBehaviour {

    /*
     * Used to track both mouse and finger touches.
     * Sends swipes to the inputhandler
     */
    CellHandler cellHandler;

    public float minSwipeLength = 50;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

	// Use this for initialization
	void Start () {
        cellHandler = (CellHandler)GameObject.Find("Cells").GetComponent("CellHandler");
	
	}
	
	// Update is called once per frame
	void Update () {
        DetectSwipe();
        DetectMouse();
	}

    public void DetectMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //Debug.Log("Mouse Press: " + firstPressPos.x + "," + firstPressPos.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //To high up on the screen
            if (Camera.main.ScreenToViewportPoint(firstPressPos).y > .75)
            {
                Debug.Log("OOB");
                return;
            }
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //Debug.Log("Mouse Release: " + secondPressPos.x + "," + secondPressPos.y);
            currentSwipe = secondPressPos - firstPressPos;

            vectorToSwipe(currentSwipe);
        }
    }

    public void DetectSwipe()
    {
        //There is a touch
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x,t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //To high up on the screen

                if (Camera.main.ScreenToViewportPoint(firstPressPos).y > .75)
                {
                    Debug.Log("OOB");
                    return;
                }
                secondPressPos = new Vector2(t.position.x, t.position.y);
                currentSwipe = secondPressPos - firstPressPos;

                vectorToSwipe(currentSwipe);
            }
        }
    }

    public void vectorToSwipe(Vector2 currentSwipe)
    {
        if (currentSwipe.magnitude > minSwipeLength)
        {
            currentSwipe.Normalize();
            //Swipe up
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                cellHandler.Swipe(Direction.UP);
            //Swipe Down
            else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                cellHandler.Swipe(Direction.DOWN);
            //Swipe Left
            else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                cellHandler.Swipe(Direction.LEFT);
            //Swipe Right
            else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                cellHandler.Swipe(Direction.RIGHT);
        }
    }
}
