using UnityEngine;
using UnityEngine.UI;

namespace Ecosystem
{
    public class Species : SimLayer
    {
        // Temperature is in C, Rainfall is in cm
        public float netCarbonEffect;
        public static double minTolerantTemp;
        public static double maxTolerantTemp;

        public static float growthFactor;
        public static float deathFactor;
        public static int width;
        public static int height;
        public static float densityScale;
        public static Color color;
        public static bool isTerrestrialCreature; // true = land animal, false = aquatic animal

        public static GameObject planet;
        internal double DeltaSeaLevel;
        
        internal Texture2D TemperatureMap;
        internal Texture2D LandMap;

        public void GenerateDensityMap()
        {
            Texture = new Texture2D(width, height);
            for (int x = 0; x < Texture.width; x++)
            {
                for (int y = 0; y < Texture.height; y++)
                {
                    float xCoord = (float)x / Texture.width * densityScale;
                    float yCoord = (float)y / Texture.height * densityScale;
                    float density = Mathf.PerlinNoise(xCoord, yCoord);
                    Color pixelColor = new Color(color.r, color.g, color.b, density);
                    Texture.SetPixel(x, y, pixelColor);
                }
            }

            Texture.Apply();
        }

        public virtual Texture2D Growth()
        {
            return new Texture2D(width, height);
        }

        public virtual Texture2D Death()
        {
            return new Texture2D(width, height);
        }

        public void PopulationChange()
        {
            Texture2D growthMap = Growth();
            Texture2D deathMap = Death();
            for (int x = 0; x < Texture.width; x++)
            {
                for (int y = 0; y < Texture.height; y++)
                {
                    Texture.SetPixel(x, y, new Color(color.r, color.g, color.b,
                        (Texture.GetPixel(x, y).grayscale + growthMap.GetPixel(x, y).grayscale -
                         deathMap.GetPixel(x, y).grayscale)));
                    
                }
            }

            Texture.Apply();
        }
        
        public void Update()
        {
            PopulationChange();
        }
    }
}
