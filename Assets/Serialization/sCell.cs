using UnityEngine;
using System.Collections;

[System.Serializable]
public class sCell{
    enum CellType
    {
        Null=0,
        Hero=1,
        Monster=2,
        Dragon=3,
        Sword=5
    }

    CellType type;
    sVector3 pos;
    int value;

    public static sCell[,] getSerArray(GameObject[,] cellArray)
    {
        sCell[,] ser = new sCell[cellArray.GetLength(0), cellArray.GetLength(1)];
        for (int x = 0; x < ser.GetLength(0); x++)
        {
            for (int y = 0; y < ser.GetLength(1); y++)
            {
                ser[x, y] = new sCell(cellArray[x, y]);
            }
        }
        return ser;
    }

    public static GameObject[,] getCellArray(sCell[,] ser)
    {
        GameObject[,] cells = new GameObject[ser.GetLength(0),ser.GetLength(1)];
        for (int x = 0; x < ser.GetLength(0); x++)
        {
            for (int y = 0; y < ser.GetLength(1); y++)
            {
                cells[x, y] = ser[x, y].getCell();
            }
        }
        return cells;
    }

    public sCell(GameObject cell)
    {
        if (cell == null)
        {
            type = CellType.Null;
            return;
        }

        pos = new sVector3(cell.transform.position);
        value = cell.GetComponent<CellValue>().Value;

        switch (cell.tag)
        {
            case "Dragon":
                type = CellType.Dragon;
                break;
            case "Hero":
                type = CellType.Hero;
                break;
            case "Monster":
                type = CellType.Monster;
                break;
            case "Sword":
                type = CellType.Sword;
                break;
        }
    }

    public GameObject getCell()
    {
        switch (type)
        {
            case CellType.Hero:
                return getHero();
            case CellType.Monster:
                return getMonster();
            case CellType.Sword:
                return getSword();
            case CellType.Dragon:
                return getDragon();
        }
        return null;
    }

    GameObject getHero()
    {
        GameObject prefab = GameObject.Instantiate(CellHandler.singleton.heroPrefab);
        prefab.transform.position = pos.getVector();
        prefab.GetComponent<CellValue>().Value = value;
        return prefab;
    }
    GameObject getMonster()
    {
        GameObject prefab = GameObject.Instantiate(CellHandler.singleton.monsterPrefab);
        prefab.transform.position = pos.getVector();
        prefab.GetComponent<CellValue>().Value = value;

        Animator anim = prefab.GetComponentInChildren<Animator>();
        for (int t = value; t > 2; t /= 3)
        {
            anim.SetTrigger("Combine");
            anim.Update(Time.deltaTime);
        }
        return prefab;
    }
    GameObject getSword()
    {
        GameObject prefab = GameObject.Instantiate(CellHandler.singleton.swordPrefab);
        prefab.transform.position = pos.getVector();
        prefab.GetComponent<CellValue>().Value = value;
        Animator anim = prefab.GetComponentInChildren<Animator>();
        for (int t = value; t > 1; t /= 3)
        {
            anim.SetTrigger("Combine");
            anim.Update(Time.deltaTime);
        }
        return prefab;
    }
    GameObject getDragon()
    {
        GameObject prefab = GameObject.Instantiate(CellHandler.singleton.dragonPrefab);
        prefab.transform.position = pos.getVector();
        prefab.GetComponent<CellValue>().Value = value;
        return prefab;
    }
}
