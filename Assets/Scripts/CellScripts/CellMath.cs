using UnityEngine;
using System.Collections;

public static class CellMath {

    /*
     * This handles the 2048 style movement.
     * 
     * Given a 2d array of cells, it returns a 2d array of ints that matches
     * the number of spaces that cell needs to move.
     * 
     * It only calculates down swipes. If the user swipes a different way,
     * it rotates the cell array to that way points down, the flips if back at the end.
     * This helps cut down on repeat code, which would make it harder to debug.
     * 
     * For example, the user swipes right. This rotates the cell array 90* clockwise,
     * performs the calculation for a downswipe, then rotate the matching int array
     * 90* counter-clockwise so it matches the right swipe.
     * 
     * Rotation is done by transposing the array and flipping the columns/rows
     */
    public static int[,] getMoveArray(GameObject[,] cellArray, Direction dir)
    {
        //cellArray
        int[,] moveArray = null;
        switch (dir)
        {
            //Rotate the cell array as needed
            case Direction.LEFT:
                //Rotate 90 CCW
                cellArray = rotateCCW(cellArray);
                //Move
                moveArray = moveDown(cellArray);
                //Rotate 90 CW
                cellArray = rotateCW(cellArray);
                moveArray = rotateCW(moveArray);
                break;
            case Direction.RIGHT:
                //Rotate 90 CW
                cellArray = rotateCW(cellArray);
                //Move
                moveArray = moveDown(cellArray);
                //Rotate 90 CCW
                cellArray = rotateCCW(cellArray);
                moveArray = rotateCCW(moveArray);
                break;
            case Direction.DOWN:
                moveArray = moveDown(cellArray);
                break;
            case Direction.UP:
                //Rotate 180
                cellArray = rotate180(cellArray);
                //Move
                moveArray = moveDown(cellArray);
                //Rotate 180
                cellArray = rotate180(cellArray);
                moveArray = rotate180(moveArray);
                break;
        }
        //Debug.Log(PrintIntArray(moveArray));
        return moveArray;
    }

    private static int[,] moveDown(GameObject[,] cellArray)
    {
        int[,] moveArray = new int[4,4];

        //For each column
        for (int x = 0; x < cellArray.GetLength(0); x++)
        {
            //Counts the freespaces in the column
            int freeSpaces = 0;
            //For each row in the column
            for (int y = 0; y < cellArray.GetLength(1); y++)
            {
                //If the tile is empty, increase the count
                if (cellArray[x, y] == null)
                {
                    freeSpaces++;
                }
                //If something is there, store the total number of empty spaces below it
                else
                {
                    moveArray[x, y] = freeSpaces;
                }
            }
        }// MoveArray created

        

        //Make a boolean array that checks if each tile will combine if slid
        bool[,] combine = new bool[4, 4];

        //For each column
        for (int x = 0; x < cellArray.GetLength(0); x++)
        {
            int combineCount = 0; //How many cells combine in this column

            //For each row in the column (skipping bottom row)
            for (int y = 1; y < cellArray.GetLength(1); y++)
            {
                //Skip empty cells
                if (cellArray[x, y] == null)
                    continue;

                //Transverse backwards from the found object
                for(int compareCell = y - 1;compareCell >= 0;compareCell--)
                {
                    //Skip empty cells
                    if (cellArray[x, compareCell] == null)
                        continue;
                    //Other cell is already combining
                    if (combine[x, compareCell] == true)
                        break;
                    
                    //Cell has been found
                    if (CellCombine.CanCombine(cellArray[x, y], cellArray[x, compareCell]))
                    {
                        combineCount++;
                        combine[x, y] = true;

                    }
                    
                    //After a cell is found, break;
                    break;
                }
                moveArray[x, y] += combineCount;
            }//Rows
        }//Columns

            return moveArray;
    }

    private static T[,] rotateCW<T>(T[,] array)
    {
        array = transposeArray(array);
        array = flipRows(array);
        return array;
    }

    private static T[,] rotateCCW<T>(T[,] array)
    {
        array = transposeArray(array);
        array = flipColumns(array);
        return array;
    }

    private static T[,] rotate180<T>(T[,] array)
    {
        array = flipColumns(array);
        array = flipRows(array);
        return array;
    }

    private static T[,] transposeArray<T>(T[,] array)
    {
        //Flip height and width
        T[,] newArray = new T[array.GetLength(1), array.GetLength(0)];

        for (int x = 0; x < array.GetLength(0); x++)
        {
            for (int y = 0; y < array.GetLength(1); y++)
            {
                newArray[y, x] = array[x, y];
            }
        }

        return newArray;
    }
    

    private static T[,] flipColumns<T>(T[,] array){
        //Make new array
        T[,] newArray = new T[array.GetLength(0),array.GetLength(1)];

        //copy the values in backwards
        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                //GetLength - 1 == last element
                newArray[x, y] = array[array.GetLength(0) - 1 - x, y];
            }
        }
            return newArray;
    }

    private static T[,] flipRows<T>(T[,] array)
    {
        //Make new array
        T[,] newArray = new T[array.GetLength(0), array.GetLength(1)];

        //copy the values in backwards
        for (int x = 0; x < array.GetLength(0); x++)
        {
            for (int y = 0; y < array.GetLength(1); y++)
            {
                //GetLength - 1 == last element
                newArray[x, y] = array[x, array.GetLength(1) - 1 - y];
            }
        }

        return newArray;
    }

    private static string PrintIntArray(int[,] a)
    {
        string cellString = "";

        for (int y = 3; y >= 0; y--)
        {
            for (int x = 0; x < a.GetLength(0); x++)
            {
                cellString += a[x,y].ToString("D4");
                cellString += " ";
                
            }
            cellString += "\n";
        }
        return cellString;
    }
}
