using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Species : MonoBehaviour
{
    public static string name;
    public static double netCarbonEffect;
    public static double minTolerantTemp;
    public static double maxTolerantTemp;
    
    protected Texture2D densityMap;

    public Planet planet;

    public void GenerateDensityMap(int width, int height, Color color)
    {
        densityMap = new Texture2D(width, height);
            float scale = 0.1f;  // Adjust the scale to control the noise pattern.
                for (int x = 0; x < densityMap.width; x++)
            {
                for (int y = 0; y < densityMap.height; y++)
                { 
                    float xCoord = (float)x / densityMap.width * scale;
                    float yCoord = (float)y / densityMap.height * scale; 
                    float density = Mathf.PerlinNoise(xCoord, yCoord);
                    Color pixelColor = new Color(color.r, color.g, color.b, density);
                    densityMap.SetPixel(x, y, pixelColor);
                }
            }
            densityMap.Apply();
    }

    public Texture2D growth(Texture2D densityMap, float growthFactor)
    {
        Texture2D dMap = densityMap;
        Color color = densityMap.GetPixel(0, 0);
        for (int x = 0; x < densityMap.width; x++)
        {
            for (int y = 0; y < densityMap.height; y++)
            {
                int count = 0;
                float val = 0;
                if (y > 0)
                {
                    val += densityMap.GetPixel(x, y - 1).grayscale;
                    count++;
                }

                if (y < densityMap.height)
                {
                    val += densityMap.GetPixel(x, y + 1).grayscale;
                    count++;
                }
                if (x > 0)
                {
                    val += densityMap.GetPixel(x - 1 , y ).grayscale;
                    count++;
                }

                if (x < densityMap.width)
                {
                    val += densityMap.GetPixel(x + 1, y ).grayscale;
                    count++;
                }

                Color newColor = new Color(color.r, color.g, color.b, growthFactor * (val / count));
                dMap.SetPixel(x,y, newColor);
            }
        }
        return dMap;
    }

    public Texture2D death(Texture2D densityMap, Texture2D predatorMap, float deathRate)
    {
        Texture2D dMap = densityMap;
        Color color = densityMap.GetPixel(0, 0);
        for (int x = 0; x < densityMap.width; x++)
        {
            for (int y = 0; y < densityMap.height; y++)
            {
                int count = 0;
                float val = 0;
                if (y > 0)
                {
                    val += densityMap.GetPixel(x, y - 1).grayscale;
                    count++;
                }

                if (y < densityMap.height)
                {
                    val += densityMap.GetPixel(x, y + 1).grayscale;
                    count++;
                }
                if (x > 0)
                {
                    val += densityMap.GetPixel(x - 1 , y ).grayscale;
                    count++;
                }

                if (x < densityMap.width)
                {
                    val += densityMap.GetPixel(x + 1, y ).grayscale;
                    count++;
                }

                Color newColor = new Color(color.r, color.g, color.b, deathRate * (val / count));
                dMap.SetPixel(x,y, newColor);
            }
        }
        return dMap;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
