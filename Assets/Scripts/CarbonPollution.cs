using System;
using System.Collections;
using System.Collections.Generic;
using Ecosystem;
using UnityEngine;
using UnityEngine.UI;

public class CarbonPollution : SimLayer
{
    // carbon content in parts per million (ppm)
    private int width;
    private int height;

    public static float TempCarbonChangeFromEnergy;

    public List<Species> species;

    public static Color color = Color.red;
    // Start is called before the first frame update
    public CarbonPollution(String name = "") : base(name)
    {
        species = new List<Species>();
        width = 1;
        height = 1;
        
        Texture = new Texture2D(width, height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Texture.SetPixel(x, y, new Color(color.r, color.g, color.b, 0f));
            }
        }
        
        // grab all sources of carbon pollution/reduction and sum their changes - currently only producers and consumers
        foreach (Species specie in species)
        {
            if (specie.netCarbonEffect >= 0)
            {
                AddWeighted(specie, specie.netCarbonEffect);
            }
            else
            {
                SubtractWeighted(specie, -1f*specie.netCarbonEffect);
            }
        }
        
        // Energy pollution is not here yet but may be added if we make this a full project more than 24 hours
        _positiveDeltaTextures.Add(EnergyPollutionTexture());
        
    }

    // Temp method because dont have power plants
    Texture2D EnergyPollutionTexture()
    {
        Texture2D emissions = new Texture2D(width, height);
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                emissions.SetPixel(x, y, new Color(color.r, color.g, color.b, 0.01f));
            }
        }

        emissions.Apply();
        return emissions;
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        ApplyDeltas();
    }

}
