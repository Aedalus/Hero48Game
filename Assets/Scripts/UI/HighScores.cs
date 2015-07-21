using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class HighScores : MonoBehaviour {

    public static string filename = "HighScores.dat";
    static HighScores singleton;

    public static bool active = false;

    public Text[] turnsText;
    public Text[] coinsText;

    public static void Display()
    {
        active = true;
        CellHandler.acceptInput = false;
        HighScoresData h = Load();

        for (int i = 0; i < 3; i++)
        {
            singleton.turnsText[i].text = h.turns[i].ToString();
            singleton.coinsText[i].text = h.coins[i].ToString();
        }
    }

    public static void Hide()
    {
        active = false;
        CellHandler.acceptInput = true;
    }


	// Use this for initialization
	void Start () {
        singleton = this;
        //Save.GetHighScores(out turnsBoard,out coinsBoard);
	}


    public static void AddNewScore(int turns, int coins)
    {
        HighScoresData h = Load();
        bool altered = false;

        for (int i = 0; i < 3; i++)
        {
            if (coins > h.coins[i])
            {
                altered = true;
                int tempCoins = h.coins[i];
                int tempTurns = h.turns[i];

                h.coins[i] = coins;
                h.turns[i] = turns;

                coins = tempCoins;
                turns = tempTurns;
            }
        }

        if (altered)
        {
            Debug.Log("New High Score!");
            Save(h);
        }
    }


    static void Save(HighScoresData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Path.Combine(Application.persistentDataPath,filename));
        bf.Serialize(file, data);
        file.Close();
    }

    public static void Reset()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Path.Combine(Application.persistentDataPath, filename));
        HighScoresData data = new HighScoresData();
        data.turns = new int[] { 0, 0, 0 };
        data.coins = new int[] { 0, 0, 0 };
        bf.Serialize(file, data);
        file.Close();
    }
    static HighScoresData Load()
    {
        //No file exists
        if(File.Exists(Path.Combine(Application.persistentDataPath,filename)) == false){
            HighScoresData h = new HighScoresData();
            h.turns = new int[] { 0, 0, 0 };
            h.coins = new int[] { 0, 0, 0 };
            return h;
        }
        else
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Path.Combine(Application.persistentDataPath, filename), FileMode.Open);
                HighScoresData h = bf.Deserialize(file) as HighScoresData;
                file.Close();
                return h;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                HighScoresData h = new HighScoresData();
                h.coins = new int[] { 0, 0, 0 };
                h.turns = new int[] { 0, 0, 0 };
                return h;
            }
        }
    }

    [Serializable]
    class HighScoresData{
        public int[] turns;
        public int[] coins;
    }
}
