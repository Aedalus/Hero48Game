using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ValueText : MonoBehaviour {

    public int value;
    CellValue cellValue;
    GameObject root;
    Text valueText;


	// Use this for initialization
	void Start () {
        root = transform.parent.parent.gameObject;
        //Debug.Log(root.name);
        cellValue = root.GetComponent<CellValue>();

        valueText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (cellValue.Value != value)
        {
            value = cellValue.Value;
            valueText.text = value.ToString();
        }
	}
}
