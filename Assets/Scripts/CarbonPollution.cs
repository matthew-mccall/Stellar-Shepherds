using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarbonPollution : MonoBehaviour
{
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
        
    }
}
