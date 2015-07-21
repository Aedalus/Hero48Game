using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinSpawner : MonoBehaviour {

    /*
     * Used to spawn the Coin pop-up.
     */
    public GameObject Coin;
    public static CoinSpawner singleton;
	// Use this for initialization
	void Start () {
        singleton = this;
	}

    public static void SpawnCoin(Vector3 pos,int amount)
    {
        GameObject coin = GameObject.Instantiate(singleton.Coin, pos, Quaternion.identity) as GameObject;
        coin.GetComponentInChildren<Text>().text += amount;
    }
}
