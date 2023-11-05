using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : Species
{
    private List<Prey> _preyList;
    public static double MinRainfall;
    public static double MaxRainfall;

    private Texture2D _temperatureMap;

    private Texture2D _rainfallMap;

    // Start is called before the first frame update
    void Start()
    {
        _temperatureMap = planet.GetComponent<Temperature>().temperatureMap;
        _rainfallMap = planet.GetComponent<Rainfall>().rainfallMap;
        GenerateDensityMap();
    }

    public Texture2D Growth()
    {
        Texture2D dMap = densityMap;
        for (int x = 0; x < densityMap.width; x++)
        {
            for (int y = 0; y < densityMap.height; y++)
            {
                if (_rainfallMap.GetPixel(x, y).grayscale > (MinRainfall / 400f) &&
                    _rainfallMap.GetPixel(x, y).grayscale < (MaxRainfall / 400f) &&
                    _temperatureMap.GetPixel(x, y).grayscale > (minTolerantTemp / 35f) &&
                    _temperatureMap.GetPixel(x, y).grayscale < (maxTolerantTemp / 35f))
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
                        val += densityMap.GetPixel(x - 1, y).grayscale;
                        count++;
                    }

                    if (x < densityMap.width)
                    {
                        val += densityMap.GetPixel(x + 1, y).grayscale;
                        count++;
                    }

                    Color newColor = new Color(color.r, color.g, color.b, growthFactor * (val / count));
                    dMap.SetPixel(x, y, newColor);
                }
                else
                {
                    Color newColor = new Color(color.r, color.g, color.b, 0);
                    dMap.SetPixel(x, y, newColor);
                }

            }
        }

        dMap.Apply();
        return dMap;
    }

    public Texture2D Death()
    {
        Texture2D dMap = densityMap;
        for (int x = 0; x < densityMap.width; x++)
        {
            for (int y = 0; y < densityMap.height; y++)
            {
                if (_rainfallMap.GetPixel(x, y).grayscale > (MinRainfall / 400f) &&
                    _rainfallMap.GetPixel(x, y).grayscale < (MaxRainfall / 400f) &&
                    _temperatureMap.GetPixel(x, y).grayscale > (minTolerantTemp / 35f) &&
                    _temperatureMap.GetPixel(x, y).grayscale < (maxTolerantTemp / 35f))
                {
                    Color newColor = new Color(color.r, color.g, color.b, 1f);
                    dMap.SetPixel(x, y, newColor);
                }
                else
                {
                    int count = 0;
                    float val = 0;

                    foreach (Prey prey in _preyList)
                    {
                        val += prey.densityMap.GetPixel(x, y).grayscale;
                        count++;
                    }

                    Color newColor = new Color(color.r, color.g, color.b, deathFactor * (val / count));
                    dMap.SetPixel(x, y, newColor);
                }


            }
        }

        return dMap;
    }

    // Update is called once per frame
    void Update()
    {
        Texture2D growthMap = Growth();
        Texture2D deathMap = Death();
        for (int x = 0; x < densityMap.width; x++)
        {
            for (int y = 0; y < densityMap.height; y++)
            {
                densityMap.SetPixel(x, y, new Color(color.r, color.g, color.b, 
                    (densityMap.GetPixel(x,y).grayscale+growthMap.GetPixel(x,y).grayscale+deathMap.GetPixel(x,y).grayscale)));
            }
        }
    }
}
