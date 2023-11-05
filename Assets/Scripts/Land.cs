using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    public Texture2D LandMap;

    public static GameObject Planet;

    public static int width;

    public static int height;

    public static Color color = Color.black;

    public static float heightScale;
    // Start is called before the first frame update
    void Start()
    {
        LandMap = new Texture2D(width, height);
        for (int x = 0; x < LandMap.width; x++)
        {
            for (int y = 0; y < LandMap.height; y++) 
            { 
                float xCoord = (float)x / LandMap.width * heightScale;
                float yCoord = (float)y / LandMap.height * heightScale; 
                float density = Mathf.PerlinNoise(xCoord, yCoord);
                Color pixelColor = new Color(color.r, color.g, color.b, density);
                LandMap.SetPixel(x, y, pixelColor);
            }
        }
        LandMap.Apply();
    }
}
