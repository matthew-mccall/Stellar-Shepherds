using UnityEngine;

namespace Climate
{
    public class Rainfall : SimLayer
    {

        public static int width;

        public static int height;

        public static int rainScale;
        public static Color color = Color.cyan;
        // Start is called before the first frame update
        void Start()
        {
            _texture = new Texture2D(width, height);
            for (int x = 0; x < _texture.width; x++)
            {
                for (int y = 0; y < _texture.height; y++) 
                { 
                    float xCoord = (float)x / _texture.width * rainScale;
                    float yCoord = (float)y / _texture.height * rainScale; 
                    float density = Mathf.PerlinNoise(xCoord, yCoord);
                    Color pixelColor = new Color(color.r, color.g, color.b, density);
                    _texture.SetPixel(x, y, pixelColor);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            // decrease each pocket of rain and moves part of that decrease somewhere else
            // randomly sets rain
        }
    }
}
