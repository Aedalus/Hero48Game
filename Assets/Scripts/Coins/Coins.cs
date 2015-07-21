using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour {

    /*
     * Keeps track of the coins gained;
     */
    public static int total;

	// Use this for initialization
	void Awake () {
        total = 0;
	}

	
	// Update is called once per frame
	void Update () {
        

        #if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.C))
        {
            total += 100;
        }
        #endif
	}

    public static void add(int i)
    {
        total += i;
    }

    public static void sub(int i)
    {
        total -= i;
    }
}
