using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarbonPollution : SimLayer
{
    // carbon content in parts per million (ppm)
    public Texture2D carbonPollutionMap;

    public static int width;

    public static int height;

    public static Color color = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        carbonPollutionMap = new Texture2D(width, height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                carbonPollutionMap.SetPixel(x, y, new Color(color.r, color.g, color.b, 0f));
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
