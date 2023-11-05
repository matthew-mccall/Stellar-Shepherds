using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : Species
{
    private List<Prey> _preysList;
    // Start is called before the first frame update
    void Start()
    {
        GenerateDensityMap();
        TemperatureMap = planet.GetComponent<Temperature>().temperatureMap;
    }
    
    public override Texture2D Growth()
    {
        Texture2D dMap = new Texture2D(width, height);
        for (int x = 0; x < densityMap.width; x++)
        {
            for (int y = 0; y < densityMap.height; y++)
            {
                float foodAvailability = 0;
                foreach (Prey prey in _preysList)
                {
                    foodAvailability += prey.densityMap.GetPixel(x, y).grayscale;
                }
                if (TemperatureMap.GetPixel(x, y).grayscale > (minTolerantTemp / 35f) &&
                    TemperatureMap.GetPixel(x, y).grayscale < (maxTolerantTemp / 35f) && 
                    foodAvailability>=growthFactor*densityMap.GetPixel(x,y).grayscale)
                {
                    Color newColor = new Color(color.r, color.g, color.b, growthFactor*(foodAvailability-growthFactor*densityMap.GetPixel(x,y).grayscale));
                }
                else
                {
                    Color newColor = new Color(color.r, color.g, color.b, 0);
                    dMap.SetPixel(x, y, newColor);
                }
            }
        } 
        return dMap;
    }
    
    public override Texture2D Death()
    {
        Texture2D dMap = new Texture2D(width, height);
        for (int x = 0; x < densityMap.width; x++)
        {
            for (int y = 0; y < densityMap.height; y++)
            {
                if (TemperatureMap.GetPixel(x, y).grayscale > (minTolerantTemp / 35f) &&
                    TemperatureMap.GetPixel(x, y).grayscale < (maxTolerantTemp / 35f))
                {
                    Color newColor = new Color(color.r, color.g, color.b, deathFactor);
                    dMap.SetPixel(x, y, newColor);
                }
                else
                {
                    Color newColor = new Color(color.r, color.g, color.b, 1f);
                    dMap.SetPixel(x, y, newColor);
                }
            }
        } 
        return dMap;
    }
    
}
