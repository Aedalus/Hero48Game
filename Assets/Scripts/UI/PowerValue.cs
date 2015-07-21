using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerValue : MonoBehaviour {

    CellValue heroValue;
    int oldValue = 0;
    Text UIText;

	// Use this for initialization
	void Start () {
        UIText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (heroValue == null)
        {
            GameObject hero = GameObject.Find("Hero(Clone)");

            if (hero != null)
            {
                //Debug.Log("Hero found");
                heroValue = hero.GetComponent(typeof(CellValue)) as CellValue;
            }
            else
                Debug.Log("No Hero Found");
        }
        else //Hero Exists
        {
            if (oldValue != heroValue.Value)
            {
                //Update Value
                oldValue = heroValue.Value;
                //Update String
                UIText.text = heroValue.Value.ToString();
            }
        }        
	}
}
