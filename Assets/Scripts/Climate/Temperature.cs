using UnityEngine;

namespace Climate
{
    public class Temperature : SimLayer
    {
        public static int width;

        public static int height;

        public static Color color;

        public static float tempScale;

        public static GameObject planet;

        public CarbonPollution CarbonPollution;
        
        // Start is called before the first frame update
        public Temperature(int width, int height) : base("Temperature", width, height)
        {
            // _carbonPollutionMap = planet.GetComponent<CarbonPollution>().carbonPollutionMap;
            for (int x = 0; x < Texture.width; x++)
            {
                for (int y = 0; y < Texture.height; y++) 
                { 
                    float xCoord = (float)x / Texture.width * tempScale;
                    float yCoord = (float)y / Texture.height * tempScale; 
                    float density = Mathf.PerlinNoise(xCoord, yCoord);
                    Color pixelColor = new Color(color.r, color.g, color.b, density);
                    Texture.SetPixel(x, y, pixelColor);
                }
            }
            for (int x = 0; x < Texture.width; x++)
            {
                for (int y = 0; y < Texture.height; y++)
                {
                    float temperature = Texture.GetPixel(x, y).r; // Extract the temperature value

                    // Modify the temperature value based on the gradient
                    float normalizedY = (float)y / Texture.height;
                    temperature *= 1.0f - Mathf.Abs(normalizedY - 0.5f) * 2.0f;

                    Texture.SetPixel(x, y, new Color(temperature, temperature, temperature));
                }
            }
            Texture.Apply();

            // AddWeighted(CarbonPollution, (float)1E-6);
        }

        
    
        // Update is called once per frame
        void Update()
        {
            ApplyDeltas();
        }
    }
}
