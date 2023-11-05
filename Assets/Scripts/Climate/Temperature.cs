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
        void Start()
        {
            // _carbonPollutionMap = planet.GetComponent<CarbonPollution>().carbonPollutionMap;
            _texture = new Texture2D(width, height);
            for (int x = 0; x < _texture.width; x++)
            {
                for (int y = 0; y < _texture.height; y++) 
                { 
                    float xCoord = (float)x / _texture.width * tempScale;
                    float yCoord = (float)y / _texture.height * tempScale; 
                    float density = Mathf.PerlinNoise(xCoord, yCoord);
                    Color pixelColor = new Color(color.r, color.g, color.b, density);
                    _texture.SetPixel(x, y, pixelColor);
                }
            }
            for (int x = 0; x < _texture.width; x++)
            {
                for (int y = 0; y < _texture.height; y++)
                {
                    float temperature = _texture.GetPixel(x, y).r; // Extract the temperature value

                    // Modify the temperature value based on the gradient
                    float normalizedY = (float)y / _texture.height;
                    temperature *= 1.0f - Mathf.Abs(normalizedY - 0.5f) * 2.0f;

                    _texture.SetPixel(x, y, new Color(temperature, temperature, temperature));
                }
            }
            _texture.Apply();

            AddWeighted(CarbonPollution, (float)1E-6);
        }

        
    
        // Update is called once per frame
        void Update()
        {
            ApplyDeltas();
        }
    }
}
