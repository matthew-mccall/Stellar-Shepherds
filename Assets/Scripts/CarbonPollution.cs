using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarbonPollution : SimLayer
{
    public static int width;

    public static int height;

    public static Color color = Color.red;
    // Start is called before the first frame update
    public CarbonPollution(String name = "") : base(name)
    {
        Texture = new Texture2D(width, height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Texture.SetPixel(x, y, new Color(color.r, color.g, color.b, 0f));
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // grab all sources of carbon pollution/reduction and sum their changes
        // Energy
    }
}
