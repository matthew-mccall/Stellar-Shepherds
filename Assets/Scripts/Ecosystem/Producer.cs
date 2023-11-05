using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : Species
{
    private List<Prey> preyList;
    public static double minRainfall;
    public static double maxRainfall;
    // Start is called before the first frame update
    void Start()
    {
        GenerateDensityMap();
    }
    
    public Texture2D growth(Texture2D temperatureMap, Texture2D rainfallMap)
    {
        Texture2D dMap = densityMap;
        Color color = densityMap.GetPixel(0, 0);
        for (int x = 0; x < densityMap.width; x++)
        {
            for (int y = 0; y < densityMap.height; y++)
            {
                if (rainfallMap.GetPixel(x, y).grayscale>(minRainfall/400f)&&rainfallMap.GetPixel(x, y).grayscale<(maxRainfall/400f)&&
                    temperatureMap.GetPixel(x, y).grayscale>(minTolerantTemp/35f)&&temperatureMap.GetPixel(x, y).grayscale<(maxTolerantTemp/35f))
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
                else
                {
                    Color newColor = new Color(color.r, color.g, color.b, 0);
                    dMap.SetPixel(x,y, newColor);
                }
                
            }
        }
        dMap.Apply();
        return dMap;
    }
    
    public Texture2D death(Texture2D predatorMap)
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

                Color newColor = new Color(color.r, color.g, color.b, deathFactor * (val / count));
                dMap.SetPixel(x,y, newColor);
            }
        }
        return dMap;
    }
    
    // Update is called once per frame
    void Update()
    {
        Texture2D growthMap = growth(planet.GetComponent<Temperature>().temperatureMap, planet.GetComponent<Rainfall>().rainfallMap);
    }
}
