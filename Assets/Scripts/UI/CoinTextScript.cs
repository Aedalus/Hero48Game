using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinTextScript : MonoBehaviour {

    /*
     * Used to update the coin number on the UI
     */
    Text coinText;
    int coinCount = 0;

	// Use this for initialization
	void Start () {
        coinText = this.GetComponent<Text>();
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Coins.total != coinCount)
        {
            coinCount = Coins.total;
            coinText.text = coinCount.ToString();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            Debug.Log("Coins: " + Coins.total);
        }
	
	}
}
