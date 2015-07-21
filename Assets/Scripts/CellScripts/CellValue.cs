using UnityEngine;
using System.Collections;

public class CellValue : MonoBehaviour {

    //Tracks the total number of swords and monsters created.
    //One power 3 sword -> SwordTotal +=3;
    public static int SwordTotal;
    public static int MonsterTotal;

    public string valueString;
    private int cellValue;

    /*
     * This get/set syntax is used so that the variable can be accessed like
     * a normal instance variable.
     * 
     * However changing it makes sure to change the value in the UI object as well.
     */
    public int Value
    {
        get
        {
            return cellValue;
        }
        set
        {
            /*If this is the first value assigned,
             * add the starting value to the total
             * value of that type
             */
            if (cellValue == 0 && this.tag != "Hero")
            {
                if (this.tag == "Monster")
                    MonsterTotal += value;
                else if (this.tag == "Sword")
                    SwordTotal += value;

            }

            //Increment the UI variables
            cellValue = value;
            valueString = value.ToString();
        }
    }
}
