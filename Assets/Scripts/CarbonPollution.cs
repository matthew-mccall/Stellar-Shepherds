using System;
using System.Collections;
using System.Collections.Generic;
using Ecosystem;
using UnityEngine;
using UnityEngine.UI;

public class CarbonPollution : SimLayer
{
    // carbon content in parts per million (ppm)
    public static float TempCarbonChangeFromEnergy;

    public static Color color = Color.red;
    
    // Start is called before the first frame update
    public CarbonPollution(int width, int height) : base("Carbon", width, height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Noise with random offset
                float xCoord = ((float)x / width * 100) + UnityEngine.Random.Range(0, 100);
                float yCoord = ((float)y / height * 100) + UnityEngine.Random.Range(0, 100);
                float density = Mathf.PerlinNoise(xCoord, yCoord);
                Color pixelColor = new Color(density, density, density, density);
            }
        }
    }
}
