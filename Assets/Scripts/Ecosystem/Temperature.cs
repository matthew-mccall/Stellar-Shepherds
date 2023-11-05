using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temperature : MonoBehaviour
{
    public Texture2D temperatureMap;

    public static int width;

    public static int height;

    public static Color color;

    public static float tempScale;
    // Start is called before the first frame update
    void Start()
    {
        temperatureMap = new Texture2D(width, height);
        for (int x = 0; x < temperatureMap.width; x++)
        {
            for (int y = 0; y < temperatureMap.height; y++) 
            { 
                float xCoord = (float)x / temperatureMap.width * tempScale;
                float yCoord = (float)y / temperatureMap.height * tempScale; 
                float density = Mathf.PerlinNoise(xCoord, yCoord);
                Color pixelColor = new Color(color.r, color.g, color.b, density);
                temperatureMap.SetPixel(x, y, pixelColor);
            }
        }
        for (int x = 0; x < temperatureMap.width; x++)
        {
            for (int y = 0; y < temperatureMap.height; y++)
            {
                float temperature = temperatureMap.GetPixel(x, y).r; // Extract the temperature value

                // Modify the temperature value based on the gradient
                float normalizedY = (float)y / temperatureMap.height;
                temperature *= 1.0f - Mathf.Abs(normalizedY - 0.5f) * 2.0f;

                temperatureMap.SetPixel(x, y, new Color(temperature, temperature, temperature));
            }
        }
        temperatureMap.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
