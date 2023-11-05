using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Biome : SimLayer
{
    public static GameObject Planet;

    protected internal static Texture2D LandMap;
    protected internal double DeltaSeaHeight;
    protected internal Texture2D RainMap;
    protected internal Texture2D TemperatureMap;
    
    public static int width;
    public static int height;
    
    public enum LandBiome
    {
        Tundra,
        Taiga,
        Woodland,
        Grassland,
        Desert,
        Forest,
        Savanna,
        TemperateRainforest,
        TropicalRainforest,
        Wasteland,
        Ocean
    }

    public Color GetColorForBiome(LandBiome biome)
    {
        switch (biome)
        {
            case LandBiome.Desert:
                return new Color(0.96f, 0.87f, 0.45f); // Yellowish color for desert
            case LandBiome.Grassland:
                return new Color(0.71f, 0.78f, 0.43f); // Greenish color for grassland
            case LandBiome.Forest:
                return new Color(0.35f, 0.53f, 0.28f); // Dark green color for forest
            case LandBiome.Taiga:
                return new Color(0.43f, 0.52f, 0.47f); // Light green color for taiga
            case LandBiome.Tundra:
                return new Color(0.78f, 0.78f, 0.74f); // Grayish color for tundra
            case LandBiome.Savanna:
                return new Color(0.8f, 0.6f, 0.2f); // Yellowish color for Savanna
            case LandBiome.TemperateRainforest:
                return new Color(0.1f, 0.3f, 0.2f); // Dark green color for Temperate Rainforest
            case LandBiome.TropicalRainforest:
                return new Color(0.0f, 0.3f, 0.0f); // Deep green color for Tropical Rainforest
            case LandBiome.Woodland:
                return new Color(0.5f, 0.4f, 0.2f); // Brownish color for Woodland
            case LandBiome.Wasteland:
                return new Color(1.0f, 0.6f, 0.0f);
            default:
                return Color.blue; // Default to blue for unknown biomes (sea)
        }
    }

    private void Start()
    {
        LandMap = Planet.GetComponent<Land>().LandMap;
        DeltaSeaHeight = Planet.GetComponent<Planet>().deltaSeaLevel;
        RainMap = Planet.GetComponent<Climate.Rainfall>()._texture;
        TemperatureMap = Planet.GetComponent<Climate.Temperature>()._texture;
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                LandBiome biome;
                if (LandMap.GetPixel(x,y).grayscale>=DeltaSeaHeight/4) {
                    
                    float temperature = TemperatureMap.GetPixel(x, y).grayscale * 35;
                    float rainfall = RainMap.GetPixel(x, y).grayscale * 400;

                    if (temperature >= 20.0f && temperature < 35.0f && rainfall >= 225.0f)
                    {
                        biome = LandBiome.TropicalRainforest;
                    }
                    else if (temperature <= 20.0f && temperature > 10.0f && rainfall >= 225.0f)
                    {
                        biome = LandBiome.TemperateRainforest;
                    }
                    else if (temperature >= 20.0f && rainfall <= 50.0f)
                    {
                        biome = LandBiome.Desert;
                    }
                    else if (temperature < 33.0f && temperature >= 20.0f && rainfall >= 50.0f && rainfall < 225.0f)
                    {
                        biome = LandBiome.Savanna;
                    }
                    else if (temperature < 1.0f && rainfall <= 100.0f)
                    {
                        biome = LandBiome.Tundra;
                    }
                    else if (temperature < 8.0f && temperature >= 1.0f && rainfall < 200.0f && rainfall >= 50.0f)
                    {
                        biome = LandBiome.Taiga;
                    }
                    else if (temperature < 20.0f && temperature >= 1.0f && rainfall < 50.0f)
                    {
                        biome = LandBiome.Grassland;
                    }
                    else if (temperature < 20.0f && temperature >= 8.0f && rainfall < 125.0f && rainfall >= 50.0f)
                    {
                        biome = LandBiome.Woodland;
                    }
                    else if (temperature < 20.0f && temperature >= 8.0f && rainfall < 225.0f && rainfall >= 125.0f)
                    {
                        biome = LandBiome.Forest;
                    }
                    else
                    {
                        biome = LandBiome.Wasteland;
                    }
                } else
                {
                    biome = LandBiome.Ocean;
                }
                _texture.SetPixel(x,y,GetColorForBiome(biome));
            }
        }
    }
}
