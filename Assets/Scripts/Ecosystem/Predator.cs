using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ecosystem
{
    public class Predator : Species
    {
        private List<Prey> _preysList;

        public Predator(List<Prey> preysList)
        {
            _preysList = preysList;
        }
    
        // Start is called before the first frame update
        void Start()
        {
            GenerateDensityMap();
            // TemperatureMap = planet.GetComponent<Temperature>().temperatureMap;
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
                    foodAvailability>=growthFactor*densityMap.GetPixel(x,y).grayscale &&
                    ((isTerrestrialCreature&&LandMap.GetPixel(x,y).grayscale>DeltaSeaLevel/4)||
                     (!isTerrestrialCreature&&LandMap.GetPixel(x,y).grayscale<DeltaSeaLevel/4)))
                {
                    Color newColor = new Color(color.r, color.g, color.b, growthFactor*(foodAvailability-growthFactor*densityMap.GetPixel(x,y).grayscale));
                    int dir = Random.Range(0, 5);
                    switch (dir)
                    {
                        case 0:
                            dMap.SetPixel(x, y, newColor);
                            break;
                        case 1:
                            if (x > 0)
                            {
                                dMap.SetPixel(x-1, y, newColor);
                            }
                            else
                            {
                                dMap.SetPixel(x, y, newColor);
                            }
                            break;
                        case 2:
                            if (y > 0)
                            {
                                dMap.SetPixel(x, y-1, newColor);
                            }
                            else
                            {
                                dMap.SetPixel(x, y, newColor);
                            }
                            break;
                        case 3:
                            if (x < width)
                            {
                                dMap.SetPixel(x+1, y, newColor);
                            }
                            else
                            {
                                dMap.SetPixel(x, y, newColor);
                            }
                            break;
                        case 4:
                            if (y < height)
                            {
                                dMap.SetPixel(x, y+1, newColor);
                            }
                            else
                            {
                                dMap.SetPixel(x, y, newColor);
                            }
                            break;
                        default:
                            dMap.SetPixel(x,y, newColor);
                            break;
                    }
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
        for (int x = 0; x < densityMap.width; x++)
        {
            for (int y = 0; y < densityMap.height; y++)
            {
                if (TemperatureMap.GetPixel(x, y).grayscale > (minTolerantTemp / 35f) &&
                    TemperatureMap.GetPixel(x, y).grayscale < (maxTolerantTemp / 35f)&&
                    ((isTerrestrialCreature&&LandMap.GetPixel(x,y).grayscale>DeltaSeaLevel/4)||
                     (!isTerrestrialCreature&&LandMap.GetPixel(x,y).grayscale<DeltaSeaLevel/4)))
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
        dMap.Apply();
        return dMap;
    }
    
    }
}
