using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour{

    /*
     * This script makes sure to limit the framerate,
     * and also loads the tutorial level if the player has not yet done it.
     */
    public void Awake()
    {
        //Set the framerate
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        //Go to tutorial if needed
        if (PlayerPrefs.GetInt("tutorial") == 0)
        {
            Application.LoadLevel("Tutorial");
        }
        //If on a PC, set the screen size
#if UNITY_STANDALONE
        Screen.SetResolution(360,640,false);
#endif
    }

}
