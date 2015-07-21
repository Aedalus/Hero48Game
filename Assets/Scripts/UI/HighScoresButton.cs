using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoresButton : MonoBehaviour {
    Text text;

    string display = "High Scores";
    string back = "Back";

    public void Start()
    {
        text = GetComponentInChildren<Text>();
        text.text = display;
    }

    public void Click()
    {
        if (HighScores.active == true)
        {
            HighScores.Hide();
        }
        else
        {
            HighScores.Display();
        }
    }

    public void Update()
    {
        if (HighScores.active == true)
            text.text = back;
        else
            text.text = display;
    }

}
