using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimLayer
{
    public Texture2D Texture { get; internal set; }
    public String Name { get; private set; }
    
    internal List<Texture2D> _positiveDeltaTextures;
    internal List<Texture2D> _negativeDeltaTextures;
    
    // Start is called before the first frame update
    public SimLayer(String name="", int width=1, int height=1)
    {
        Texture = new Texture2D(width, height);
        Name = name;
        _positiveDeltaTextures = new List<Texture2D>();
        _negativeDeltaTextures = new List<Texture2D>();
    }
    
    internal SimLayer AddWeighted(SimLayer other, float weight)
    {
        Texture2D deltaTexture = new Texture2D(Texture.width, Texture.height);
        
        for (int x = 0; x < Texture.width; x++)
        {
            for (int y = 0; y < Texture.height; y++)
            {
                deltaTexture.SetPixel(x, y, other.Texture.GetPixel(x, y) * weight);
            }
        }
        
        _positiveDeltaTextures.Add(deltaTexture);
        return this;
    }
    
    internal SimLayer SubtractWeighted(SimLayer other, float weight)
    {
        Texture2D deltaTexture = new Texture2D(Texture.width, Texture.height);
        
        for (int x = 0; x < Texture.width; x++)
        {
            for (int y = 0; y < Texture.height; y++)
            {
                deltaTexture.SetPixel(x, y, other.Texture.GetPixel(x, y) * weight);
            }
        }
        
        _negativeDeltaTextures.Add(deltaTexture);
        return this;
    }

    SimLayer DistributeEvenly(float speed)
    {
        Texture2D deltaTexture = new Texture2D(Texture.width, Texture.height);
        
        for (int x = 0; x < Texture.width; x++)
        {
            for (int y = 0; y < Texture.height; y++)
            {
                float val = Texture.GetPixel(x, y).grayscale;
                float valLeft = 0;
                float valRight = 0;
                float valUp = 0;
                float valDown = 0;
                if (x > 0)
                {
                    valLeft = Texture.GetPixel(x - 1, y).grayscale;
                }

                if (x < Texture.width - 1)
                {
                    valRight = Texture.GetPixel(x + 1, y).grayscale;
                }

                if (y > 0)
                {
                    valDown = Texture.GetPixel(x, y - 1).grayscale;
                }

                if (y < Texture.height - 1)
                {
                    valUp = Texture.GetPixel(x, y + 1).grayscale;
                }

                float valTotal = val + valLeft + valRight + valUp + valDown;
                float valAverage = valTotal / 5;
                float valDelta = valAverage - val;
                valDelta *= speed;
                deltaTexture.SetPixel(x, y, new Color(valDelta, valDelta, valDelta, 0));
            }
        }
        
        _positiveDeltaTextures.Add(deltaTexture);
        return this;
    }

    SimLayer Mask(SimLayer other, float cutoff)
    {
        Texture2D deltaTexture = new Texture2D(Texture.width, Texture.height);
        
        for (int x = 0; x < Texture.width; x++)
        {
            for (int y = 0; y < Texture.height; y++)
            {
                float val = Texture.GetPixel(x, y).grayscale;
                float otherVal = other.Texture.GetPixel(x, y).grayscale;
                if (otherVal < cutoff)
                {
                    deltaTexture.SetPixel(x, y, new Color(val, val, val, 0));
                }
                else
                {
                    deltaTexture.SetPixel(x, y, new Color(0, 0, 0, 0));
                }
            }
        }
        
        _positiveDeltaTextures.Add(deltaTexture);
        return this;
    }
    
    internal void ApplyDeltas()
    {
        foreach (Texture2D deltaTexture in _positiveDeltaTextures)
        {
            for (int x = 0; x < Texture.width; x++)
            {
                for (int y = 0; y < Texture.height; y++)
                {
                    Texture.SetPixel(x, y, Texture.GetPixel(x, y) + deltaTexture.GetPixel(x, y));
                }
            }
        }
        
        foreach (Texture2D deltaTexture in _negativeDeltaTextures)
        {
            for (int x = 0; x < Texture.width; x++)
            {
                for (int y = 0; y < Texture.height; y++)
                {
                    Texture.SetPixel(x, y, Texture.GetPixel(x, y) - deltaTexture.GetPixel(x, y));
                }
            }
        }
    }

    public float GetAverage()
    {
        float sum = Texture.GetPixels().Sum(pixel => pixel.grayscale);
        return sum / (Texture.width * Texture.height);
    }
}
