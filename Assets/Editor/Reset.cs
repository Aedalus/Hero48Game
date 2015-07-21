using UnityEngine;
using UnityEditor;
using System.Collections;

public class Reset{

    [MenuItem("HighScores/Reset HighScores")]
    public static void ResetHighScores()
    {
        HighScores.Reset();
    }

    [MenuItem("HighScores/Delete Save")]
    public static void DeleteSave()
    {
        //Save.DeleteSave();
    }

    [MenuItem("HighScores/ClearPlayerPrefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.SetInt("tutorial", 0);
    }

    [MenuItem("HighScores/CleanLaunch")]
    public static void CleanLaunch()
    {
        ResetHighScores();
        ClearPrefs();
    }
}
