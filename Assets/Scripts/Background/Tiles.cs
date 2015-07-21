using UnityEngine;
using System.Collections;

public class Tiles: MonoBehaviour {

    /*
     * This script spawns the tile objects manually,
     * and spaces them out. This is poor design, and I should have used
     * the new UI to do this, as it would have been much easier.
     */
    public static Vector3[,] coordinates;
    public GameObject Tile;
    public static Vector3 BotLeft;
    public static Vector3 TopRight;
    public static float tileDistance;

    private static float margin = .2f;
    private static float tileSize;
    private static GameObject[,] tileGrid;
    
    void Awake()
    {
        //Get size of sprite
        Renderer rend = (Renderer)Tile.GetComponent(typeof(Renderer));
        tileSize = rend.bounds.size.x;
        //Debug.Log("TileSize: " + tileSize);
        //Create Arrays
        coordinates = new Vector3[4, 4];
        tileGrid = new GameObject[4, 4];

        //Initialize Arrays
        initArrays();

        //Update edges
        BotLeft = coordinates[0, 0];
        BotLeft.x -= (float)tileSize / 2;
        BotLeft.y -= (float)tileSize / 2;

        TopRight = coordinates[3, 3];
        TopRight.x += (float)tileSize / 2;
        TopRight.y += (float)tileSize / 2;

        //The distance between two tile
        tileDistance = tileSize + margin;
        //Debug.Log("tileDistance: " + tileDistance);

    }

	// Use this for initialization
	void Start () {

      
	}

    public void initArrays()
    {
        Vector2 bottomLeft = new Vector2();
        bottomLeft.x = (float)(transform.position.x - margin - margin/2 - tileSize - tileSize/2);
        //bottomLeft.x = (float)(transform.position.x - tileSize - margin - tileSize - margin / 2);
        bottomLeft.y = bottomLeft.x + transform.position.y;
        

        for (int x = 0; x < coordinates.GetLength(0); x++)
        {
            float column = (float)(bottomLeft.x + (tileSize + margin) * x);
            for (int y = 0; y < coordinates.GetLength(1); y++)
            {
                float row = (float)(bottomLeft.y + (tileSize + margin) * y);

                coordinates[x, y] = new Vector3(column, row, 0);

                tileGrid[x, y] = (GameObject)Instantiate(Tile);
                tileGrid[x, y].transform.position = coordinates[x, y];
                tileGrid[x, y].transform.SetParent(this.gameObject.transform);

                //Debug.Log("Tile created at: " + column + "," + row);
            }
        }
    }

    /// <summary>
    /// Returns a vector2 of the cell this object rests on.
    /// </summary>
    /// <param name="vec"></param>
    /// <returns>A Vector2 where x any y are the cell coordinates, range 0-3.
    /// If it is not found, then -1,-1 is returned.</returns>
    public static Vector2 getCellNumber(Vector3 vec)
    {
        for (int x = 0; x < coordinates.GetLength(0); x++)
        {
            for (int y = 0; y < coordinates.GetLength(1); y++)
            {
                if (vec == coordinates[x, y])
                    return new Vector2(x, y);
            }
        }
        return new Vector2(-1,-1);
    }
	// Update is called once per frame
	void Update () {
	
	}


}
