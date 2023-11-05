using UnityEngine;

namespace Ecosystem
{
    public class Species : MonoBehaviour
    {
        // Temperature is in C, Rainfall is in cm
        public static double netCarbonEffect;
        public static double minTolerantTemp;
        public static double maxTolerantTemp;
    
        public static float growthFactor;
        public static float deathFactor;
        public static int width;
        public static int height;
        public static float densityScale;
        public static Color color;
    
        protected internal Texture2D densityMap;
    
        public static GameObject planet;
        internal Texture2D TemperatureMap;

        public void GenerateDensityMap()
        {
            densityMap = new Texture2D(width, height);
            for (int x = 0; x < densityMap.width; x++)
            {
                for (int y = 0; y < densityMap.height; y++) 
                { 
                    float xCoord = (float)x / densityMap.width * densityScale;
                    float yCoord = (float)y / densityMap.height * densityScale; 
                    float density = Mathf.PerlinNoise(xCoord, yCoord);
                    Color pixelColor = new Color(color.r, color.g, color.b, density);
                    densityMap.SetPixel(x, y, pixelColor);
                }
            }
            densityMap.Apply();
        }

        public virtual Texture2D Growth()
        {
            return new Texture2D(width, height);
        }

        public virtual Texture2D Death()
        {
            return new Texture2D(width, height);
        }

        public void Update()
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
}
