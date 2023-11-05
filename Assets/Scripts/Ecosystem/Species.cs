using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Species : MonoBehaviour
{
    // Temperature is in C, Rainfall is in cm
    public static double netCarbonEffect;
    public static double minTolerantTemp;
    public static double maxTolerantTemp;
    
    public static float growthFactor;
    public static float deathFactor;
    public static int width;
    public static int height;
    public static float densityScale;
    public static Color color;
    
    protected internal Texture2D densityMap;
    
    public static GameObject planet;

    public void GenerateDensityMap()
    {
        densityMap = new Texture2D(width, height);
        for (int x = 0; x < densityMap.width; x++)
        {
            for (int y = 0; y < densityMap.height; y++) 
            { 
                float xCoord = (float)x / densityMap.width * densityScale;
                float yCoord = (float)y / densityMap.height * densityScale; 
                float density = Mathf.PerlinNoise(xCoord, yCoord);
                Color pixelColor = new Color(color.r, color.g, color.b, density);
                densityMap.SetPixel(x, y, pixelColor);
            }
        }
        densityMap.Apply();
    }
}
