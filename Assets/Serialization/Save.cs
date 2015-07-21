using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Save : MonoBehaviour {

    static string filename = "savegame.dat";

    public void OnApplicationQuit()
    {
        SaveData();
    }

    public void OnApplicationFocus(bool focus)
    {
        
        if (focus == false)
            SaveData();
    }

    public static void DeleteData()
    {
        string path = Path.Combine(Application.persistentDataPath, filename);
        File.Delete(path);
    }
    
    public static void SaveData()
    {
        if (CellHandler.gameOver == true)
            return;

        BinaryFormatter bf = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath,filename);
        FileStream file = File.Create(path);

        Data data = new Data();
        data.coins = Coins.total;
        data.turns = CellHandler.turns;
        data.SwordPower = CellValue.SwordTotal;
        data.MonsterPower = CellValue.MonsterTotal;
        data.cells = sCell.getSerArray(CellHandler.cellArray);

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Data saved");
    }

    public static bool LoadData()
    {
        string path = Path.Combine(Application.persistentDataPath, filename);
        if (File.Exists(path) == false)
        {
            Debug.Log("No Save Exists");
            return false;
        }

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            Data data = bf.Deserialize(file) as Data;

            Coins.total = data.coins;
            CellHandler.turns = data.turns;
            GameObject[,] cells = sCell.getCellArray(data.cells);
            CellHandler.singleton.setParentForCells(cells);
            CellValue.SwordTotal = data.SwordPower;
            CellValue.MonsterTotal = data.MonsterPower;
            file.Close();
            Debug.Log("Game Loaded");
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    [Serializable]
    class Data
    {
        public int coins;
        public int turns;
        public int SwordPower;
        public int MonsterPower;

        public sCell[,] cells;
    }
}
