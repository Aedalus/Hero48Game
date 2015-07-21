using UnityEngine;
using System.Collections;

public class CoinAnim : MonoBehaviour {

    /*
     * A simple animation that makes the coinUI float up a bit after spawning
     */
    Vector3 dest;
    float speed = 2;
	// Use this for initialization
	void Start () {
        dest = transform.position + Vector3.up;
	}
	
	// Update is called once per frame
	void Update () {
        if (dest != transform.position)
            transform.position = Vector3.MoveTowards(transform.position, dest, Time.deltaTime * speed);
        else
            GameObject.Destroy(this.gameObject);
	}
}
