using UnityEngine;
using System.Collections;

public enum Direction { UP, DOWN, LEFT, RIGHT }

public class CellHandler : MonoBehaviour
{
    /*
     * Largest class in the game. Keeps track of all the cells,
     * and spawns new cells each turn;
     */

    //Rough singleton pattern used
    public static CellHandler singleton;

    //Default values cells spawn with
    public static int swordSmallValue = 1;
    public static int swordMidValue = swordSmallValue * 3;
    public static int monsterSmallValue = 2;
    public static int monsterMidValue = monsterSmallValue * 3;

    //Rubberbanding for cell spawns
    public static float rubberbandUpper = 0.7f; //Higher proportion spawns enemies
    public static float rubberbandLower = 0.5f; //Lower proportion spawns swords

    //Cell prefabs
    public GameObject swordPrefab;
    public GameObject monsterPrefab;
    public GameObject heroPrefab;
    public GameObject dragonPrefab;

    public GameObject GameOverText;
    public GameObject Logo;

    //Array to hold cells
    public static GameObject[,] cellArray;

    //Used to check if any cells are still moving
    bool readyForInput = false;

    //Used by other objects to lock cellHandler
    public static bool acceptInput = true;
    public static bool gameOver = false;

    //The turns in the game
    public static int turns = 0;


    // Use this for initialization
    void Start()
    {
        singleton = this;
        //Try to load any save
        if (Save.LoadData())
        {
            readyForInput = true;
        }
        else
        {
            Restart();
        }
    }

    //Resets values to start
    public void Restart()
    {
        GameOverSwitch.ToLogo();
        gameOver = false;
        //Delete all existing Cells
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //Initialize variables
        cellArray = new GameObject[4, 4];
        readyForInput = false;
        acceptInput = true;
        Coins.total = 0;
        turns = 0;

        //Spawn first Cell
        GameObject hero = (GameObject)Instantiate(heroPrefab);
        CellValue v = (CellValue)hero.GetComponent(typeof(CellValue));
        v.Value = 5;
        placeObjRandomly(hero);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.V))
        {
            Debug.Log("Swords " + CellValue.SwordTotal + " | Monsters: " + CellValue.MonsterTotal);
        }
#endif
        if (readyForInput == false)
        {
            //Right after the cells got to their targets
            if (cellsAreMoving() == false)
            {
                //Spawn a new cell
                GameObject cell = getRandomCell();
                placeObjRandomly(cell);
                readyForInput = true;
            }
        }
    }

    //Handles swipes from the input handler
    public void Swipe(Direction dir)
    {
        if (readyForInput == true && acceptInput == true)
        {
            //Get the moveArray
            int[,] moveArray = CellMath.getMoveArray(cellArray, dir);

            //Check if anything will move

            bool willMove = false;
            for (int x = 0; x < moveArray.GetLength(0); x++)
            {
                for (int y = 0; y < moveArray.GetLength(1); y++)
                {
                    willMove = willMove || (moveArray[x, y] > 0);
                }
            }

            if (willMove == false)
                return;

            if (willMove == true)
            {
                turns++;
                readyForInput = false;

                //For all cells
                for (int y = 3; y >= 0; y--)
                {
                    for (int x = 0; x < cellArray.GetLength(0); x++)
                    {
                        if (cellArray[x, y] != null)
                        {
                            CellMovement cellMove
                                = (CellMovement)cellArray[x, y].GetComponent(typeof(CellMovement));

                            cellMove.Flip(dir);

                            if (moveArray[x, y] > 0)
                            {
                                cellMove.move(dir, moveArray[x, y]);
                                //Remove from cellhandler
                                cellArray[x, y] = null;
                            }
                        }
                    }
                }
            }
        }
    }

    bool cellsAreMoving()
    {
        Component[] cellArray = GetComponentsInChildren(typeof(CellMovement));
        foreach (CellMovement cell in cellArray)
        {
            if (cell.isMoving() == true)
                return true;
        }
        //None are moving
        return false;
    }

    //Spawns a random Cell
    GameObject getRandomCell()
    {
        //Will be assigned and returned
        GameObject cell = null;

        //Dragon Spawning every 50 turns
        if (turns % 50 == 0 && turns != 0)
        {
            cell = (GameObject)Instantiate(dragonPrefab);
            CellValue c = (CellValue)cell.GetComponent(typeof(CellValue));
            c.Value = 25 + 5 * (turns / 50);
            return cell;
        }

        //Rubberbanding happens after 5 turns
        if (turns > 5)
        {
            float swordProportion = (float)CellValue.SwordTotal / (CellValue.SwordTotal + CellValue.MonsterTotal);
            //Debug.Log("SwordProportion: " + swordProportion);

            //Too many boons
            if (swordProportion > rubberbandUpper)
            {
                //Create Monster
                cell = (GameObject)Instantiate(monsterPrefab);
                CellValue c = (CellValue)cell.GetComponent(typeof(CellValue));
                c.Value = monsterSmallValue;
                return cell;
            }
            //Too few boons
            if (swordProportion < rubberbandLower)
            {
                //Create sword
                cell = (GameObject)Instantiate(swordPrefab);
                CellValue v = (CellValue)cell.GetComponent(typeof(CellValue));
                v.Value = swordMidValue;
                Animator anim = cell.GetComponentInChildren<Animator>();
                anim.SetTrigger("Combine");
                return cell;
            }
        }

        //Randomly make a cell
        int rand = Random.Range(0, 10);
        switch (rand)
        {
            case 0:
                cell = (GameObject)Instantiate(swordPrefab);
                CellValue v = (CellValue)cell.GetComponent(typeof(CellValue));
                v.Value = swordMidValue;
                Animator anim = cell.GetComponentInChildren<Animator>();
                anim.SetTrigger("Combine");
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                cell = (GameObject)Instantiate(swordPrefab);
                CellValue b = (CellValue)cell.GetComponent(typeof(CellValue));
                b.Value = swordSmallValue;
                break;
            case 7:
            case 8:
            case 9:
                cell = (GameObject)Instantiate(monsterPrefab);
                CellValue c = (CellValue)cell.GetComponent(typeof(CellValue));

                c.Value = monsterSmallValue;
                break;
        }
        return cell;
    }

    void placeObjRandomly(GameObject obj)
    {
        //Max is not inclusive, so 17 rather than 16
        int placeNumber = Random.Range(1, 17 - transform.childCount);
        //Put object into group
        obj.transform.SetParent(transform);

        //Loop through cells. After /placenumber/ empty cells are crossed,
        //then place the obj
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (cellArray[x, y] == null)
                {
                    if (placeNumber > 1)
                        placeNumber--;
                    else
                    {
                        obj.transform.position = Tiles.coordinates[x, y];
                        cellArray[x, y] = obj;
                        return;
                    }
                }
            }
        }
    }

    //Stores a cell in the array
    public static void storeCell(GameObject cell)
    {
        Vector2 tile = Tiles.getCellNumber(cell.transform.position);

        if (cellArray[(int)tile.x, (int)tile.y] != null)
        {
            cell = CellCombine.Combine(cell, cellArray[(int)tile.x, (int)tile.y]);
        }

        cellArray[(int)tile.x, (int)tile.y] = cell;
    }

    //Can print the current cells for debugging
    public string PrintCells()
    {
        string cellString = "";

        for (int y = 3; y >= 0; y--)
        {
            for (int x = 0; x < cellArray.GetLength(0); x++)
            {
                if (cellArray[x, y] == null)
                {
                    cellString += "null   ";
                }
                else
                {
                    CellValue cellVal = (CellValue)cellArray[x, y].GetComponent(typeof(CellValue));
                    cellString += cellVal.Value.ToString("D4");
                    cellString += " ";
                }
            }
            cellString += "\n";
        }
        return cellString;
    }

    //Used when loading a game. Replaces the current cellArray with the new one
    public void setParentForCells(GameObject[,] cells)
    {
        //Destroy any current children
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        CellHandler.cellArray = cells;
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                if (cells[x, y] != null)
                    cells[x, y].transform.SetParent(this.transform);
            }
        }
    }

    //Forces all cells to their target positions, then returns the cellArray for saving
    public GameObject[,] getCellsForSave()
    {
        //Move each cell to it's destination
        CellMovement[] cellMoves = GetComponentsInChildren<CellMovement>();
        foreach (CellMovement m in cellMoves)
        {
            m.EmergencyStep();
        }
        return CellHandler.cellArray;
    }
}
