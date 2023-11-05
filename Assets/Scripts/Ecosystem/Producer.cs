using System.Collections.Generic;
using Climate;
using UnityEngine;

namespace Ecosystem
{
    public class Producer : Species
    {
        private List<Prey> _preyList;
        public static double MinRainfall;
        public static double MaxRainfall;

        private Texture2D _rainfallMap;

    // Start is called before the first frame update
    void Start()
    {
        TemperatureMap = planet.GetComponent<Temperature>().Texture;
        LandMap = planet.GetComponent<Land>().LandMap;
        _rainfallMap = planet.GetComponent<Rainfall>().Texture;
        DeltaSeaLevel = planet.GetComponent<Planet>().deltaSeaLevel;
        GenerateDensityMap();
    }

    public override Texture2D Growth()
    {
        Texture2D dMap = new Texture2D(width, height);
        for (int x = 0; x < Texture.width; x++)
        {
            for (int y = 0; y < Texture.height; y++)
            {
                
                if (_rainfallMap.GetPixel(x, y).grayscale > (MinRainfall / 400f) &&
                    _rainfallMap.GetPixel(x, y).grayscale < (MaxRainfall / 400f) &&
                    TemperatureMap.GetPixel(x, y).grayscale > (minTolerantTemp / 35f) &&
                    TemperatureMap.GetPixel(x, y).grayscale < (maxTolerantTemp / 35f)&&
                    ((isTerrestrialCreature&&LandMap.GetPixel(x,y).grayscale>DeltaSeaLevel/4)||
                     (!isTerrestrialCreature&&LandMap.GetPixel(x,y).grayscale<DeltaSeaLevel/4)))
                {
                    int count = 0;
                    float val = 0;
                    if (y > 0)
                    {
                        val += Texture.GetPixel(x, y - 1).grayscale;
                        count++;
                    }

                        if (y < Texture.height)
                        {
                            val += Texture.GetPixel(x, y + 1).grayscale;
                            count++;
                        }

                        if (x > 0)
                        {
                            val += Texture.GetPixel(x - 1, y).grayscale;
                            count++;
                        }

                        if (x < Texture.width)
                        {
                            val += Texture.GetPixel(x + 1, y).grayscale;
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

    public override Texture2D Death()
    {
        Texture2D dMap = new Texture2D(width, height);
        for (int x = 0; x < Texture.width; x++)
        {
            for (int y = 0; y < Texture.height; y++)
            {
                if (_rainfallMap.GetPixel(x, y).grayscale > (MinRainfall / 400f) &&
                    _rainfallMap.GetPixel(x, y).grayscale < (MaxRainfall / 400f) &&
                    TemperatureMap.GetPixel(x, y).grayscale > (minTolerantTemp / 35f) &&
                    TemperatureMap.GetPixel(x, y).grayscale < (maxTolerantTemp / 35f)&&
                    ((isTerrestrialCreature&&LandMap.GetPixel(x,y).grayscale>DeltaSeaLevel/4)||
                     (!isTerrestrialCreature&&LandMap.GetPixel(x,y).grayscale<DeltaSeaLevel/4)))
                {
                    float val = 0;
                    foreach (Prey prey in _preyList)
                    {
                        val += prey.Texture.GetPixel(x, y).grayscale;
                    }
                    Color newColor = new Color(color.r, color.g, color.b, deathFactor * (val));
                    dMap.SetPixel(x, y, newColor);
                }
                else
                {
                    Color newColor = new Color(color.r, color.g, color.b, 1);
                    dMap.SetPixel(x, y, newColor);
                }
            }
        }
        dMap.Apply();
        return dMap;
    }

        // Update is called once per frame
        /*void Update()
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
    }*/
    }
}
