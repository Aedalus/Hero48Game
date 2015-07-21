using UnityEngine;
using System.Collections;

public static class CellCombine {

    /*
     * This class is used to handle what happens if two cells combine
     */

    /// <summary>
    /// Returns true if the two gameobjects can combine, based on their tags.
    /// </summary>
    /// <param name="one">The First GameObject</param> 
    /// <param name="two">The Second GameObject</param>
    /// <returns></returns>
    public static bool CanCombine(GameObject one, GameObject two)
    {
        //Either is the hero
        if (one.tag == "Hero" || two.tag == "Hero")
            return true;
        else if (one.tag == two.tag)
        {
            CellValue oneV = (CellValue)one.GetComponent(typeof(CellValue));
            CellValue twoV = (CellValue)two.GetComponent(typeof(CellValue));

            if (oneV.Value == twoV.Value)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Combines the two given cells. Returns the one that remains
    /// </summary>
    /// <param name="a">The first cell</param>
    /// <param name="b">The second cell</param>
    /// <returns>The resulting cell of the combine</returns>
    public static GameObject Combine(GameObject a, GameObject b)
    {
        //If either is a hero,use a different method
        if (a.tag == "Hero")
            return CombineHero(a, b);
        if (b.tag == "Hero")
            return CombineHero(b, a);

        //Destroy one of the objects
        Object.Destroy(b);

        //Times the value of the other by 3
        CellValue cellVal = (CellValue)a.GetComponent(typeof(CellValue));
        cellVal.Value *= 3;
        //Increase sprite
        a.GetComponentInChildren<Animator>().SetTrigger("Combine");
        return a;
    }

    /// <summary>
    /// Used to combine cells with the Hero
    /// </summary>
    /// <param name="hero">The Hero Cell</param>
    /// <param name="other">The other cell</param>
    /// <returns>The hero cell</returns>
    private static GameObject CombineHero(GameObject hero,GameObject other){

        //get the two value components
        CellValue heroV = (CellValue)hero.GetComponent(typeof(CellValue));
        CellValue otherV = (CellValue)other.GetComponent(typeof(CellValue));

        //If combining with a monster, reduce the hero value
        if (other.tag == "Monster")
        {
            heroV.Value -= otherV.Value;
            //Add coins
            Coins.add(otherV.Value);
            CoinSpawner.SpawnCoin(other.transform.position,otherV.Value);
        }
        //If combining with a sword, increase the hero value
        else if (other.tag == "Sword")
        {
            heroV.Value += otherV.Value;
        }
        //If combining with a dragon, reduce the hero value
        else if (other.tag == "Dragon"){
            heroV.Value -= otherV.Value;
            //Add coins
            Coins.add(otherV.Value * 2);
            CoinSpawner.SpawnCoin(other.transform.position,otherV.Value * 2);
        }
            
        else
            Debug.Log("Error Combining with hero: " + hero.tag + "/" +
                other.tag);

        //Check to see if combining killed the Hero
        if (heroV.Value < 1)
        {
            //Set gameover values
            CellHandler.acceptInput = false;
            CellHandler.gameOver = true;
            //Delete the save
            Save.DeleteData();

            //Go to one ot the two gameover screens
            if (Coins.total >= 2048)
                GameOverSwitch.ToPyrrhic();
            else
                GameOverSwitch.ToGameOver();

            //Add a highscore
            HighScores.AddNewScore(CellHandler.turns, Coins.total);
                
        }
        //Check if combining won the game
        else if (Coins.total >= 2048)
        {
            GameOverSwitch.ToWin();
        }

        //Destroy the non hero, return the hero
        Object.Destroy(other);
        return hero;
    }
}
