using UnityEngine;
using System.Collections;

public class CellMovement : MonoBehaviour {

    private Vector3 target;
    public static float speed = 15;
    private GameObject spriteHandler;

    //Immediately moves to the target position and combines if needed
    public void EmergencyStep()
    {
        if (transform.position != target)
        {
            transform.position = target;
            CellHandler.storeCell(this.gameObject);
        }
    }

	// Use this for initialization
	void Start () {
        target = transform.position;
        spriteHandler = transform.Find("SpriteHandler").gameObject;
	}

	// Update is called once per frame
	void FixedUpdate () {
        
        //If not at the target position, move there
	    if((target == transform.position) == false){

            transform.position = Vector3.Lerp(transform.position, target, Time.fixedDeltaTime * speed);    

            //Was the final step to the target position. .01 is close enough
            if (Vector3.Distance(transform.position, target) < 0.01)
            {
                transform.position = target;
                CellHandler.storeCell(this.gameObject);   
            }
        }
	}

    //Gets a direction and a number, and sets the new target
    //for that many spaces in the given direction
    public void move(Direction dir,int spaces){

        float totalDistance = Tiles.tileDistance * spaces;
        //Debug.Log("Distane moved: " + totalDistance);
        
        switch (dir)
        {
            case Direction.UP:
                target += new Vector3(0, totalDistance, 0);
                break;
            case Direction.DOWN:
                target -= new Vector3(0, totalDistance, 0);
                break;
            case Direction.LEFT:
                target -= new Vector3(totalDistance, 0, 0);
                break;
            case Direction.RIGHT:
                target += new Vector3(totalDistance, 0, 0);
                break;
        }
    }

    //Flips the sprite the right way
    public void Flip(Direction dir)
    {
        if (dir == Direction.UP || dir == Direction.DOWN)
            return;
        if (this.CompareTag("Sword"))
            return;
        //Debug.Log("Flip Test: " + this.tag);
        //Everything else flips
        switch (dir)
        {
            case Direction.LEFT:
                //already facing right
                if (spriteHandler.transform.localScale.x < 0)
                {
                    Vector3 scale = spriteHandler.transform.localScale;
                    scale.x *= -1;
                    spriteHandler.transform.localScale = scale;
                }
                break;
            case Direction.RIGHT:
                //already facing left
                if (spriteHandler.transform.localScale.x > 0)
                {
                    Vector3 scale = spriteHandler.transform.localScale;
                    scale.x *= -1;
                    spriteHandler.transform.localScale = scale;
                }
                break;
        }
    }

    public bool isMoving()
    {
        if (target == transform.position)
            return false;
        else
            return true;
    }
}
