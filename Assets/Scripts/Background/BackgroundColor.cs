using UnityEngine;
using System.Collections;

public class BackgroundColor : MonoBehaviour {

    /*
     * This script finds the color of the top left pixel of an image,
     * then sets the background color to the same color.
     * 
     * This way anything shown above the background appears to be the sky.
     */
    public Sprite background;
    private Color color;

	// Use this for initialization
	void Start () {
        Texture2D texture = background.texture;

        color = texture.GetPixel(0, texture.height);
        GetComponent<Camera>().backgroundColor = color;
	
	}
}
