using UnityEngine;
using System.Collections;

public class GameOverSwitch : MonoBehaviour {

    public GameObject Logo;
    public GameObject GameOver;
    public GameObject YouWon;
    public GameObject Pyrrhic;

    public static GameOverSwitch singleton;

    public void Awake()
    {
        singleton = this;
    }


    public static void ToGameOver()
    {
        singleton.Logo.SetActive(false);
        singleton.GameOver.SetActive(true);
        singleton.YouWon.SetActive(false);
        singleton.Pyrrhic.SetActive(false);
    }

    public static void ToLogo()
    {
        singleton.Logo.SetActive(true);
        singleton.GameOver.SetActive(false);
        singleton.YouWon.SetActive(false);
        singleton.Pyrrhic.SetActive(false);
    }
    public static void ToWin()
    {
        singleton.Logo.SetActive(false);
        singleton.GameOver.SetActive(false);
        singleton.YouWon.SetActive(true);
        singleton.Pyrrhic.SetActive(false);
    }
    public static void ToPyrrhic()
    {
        singleton.Logo.SetActive(false);
        singleton.GameOver.SetActive(false);
        singleton.YouWon.SetActive(false);
        singleton.Pyrrhic.SetActive(true);
    }
}
