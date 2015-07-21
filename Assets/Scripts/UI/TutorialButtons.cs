using UnityEngine;
using System.Collections;

public class TutorialButtons : MonoBehaviour {

    public int page = 0;

    public void next()
    {
        if (page + 1 == transform.childCount)
            return;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        page++;
        transform.GetChild(page).gameObject.SetActive(true);
    }

    public void previous()
    {
        if (page == 0)
            return;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        page--;
        transform.GetChild(page).gameObject.SetActive(true);
    }

    public void ToGame()
    {
        PlayerPrefs.SetInt("tutorial", 1);
        Application.LoadLevel("GameScene");
    }
}
