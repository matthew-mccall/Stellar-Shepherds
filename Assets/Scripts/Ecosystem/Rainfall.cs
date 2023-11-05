using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainfall : MonoBehaviour
{
    public Texture2D rainfallMap;

    public static int width;

    public static int height;

    public static int rainScale;
    public static Color color = Color.cyan;
    // Start is called before the first frame update
    void Start()
    {
        rainfallMap = new Texture2D(width, height);
        for (int x = 0; x < rainfallMap.width; x++)
        {
            for (int y = 0; y < rainfallMap.height; y++) 
            { 
                float xCoord = (float)x / rainfallMap.width * rainScale;
                float yCoord = (float)y / rainfallMap.height * rainScale; 
                float density = Mathf.PerlinNoise(xCoord, yCoord);
                Color pixelColor = new Color(color.r, color.g, color.b, density);
                rainfallMap.SetPixel(x, y, pixelColor);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // decrease each pocket of rain and moves part of that decrease somewhere else
        // randomly sets rain
    }
}
