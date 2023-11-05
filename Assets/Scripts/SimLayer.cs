using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimLayer : MonoBehaviour
{
    protected internal Texture2D _texture;
    protected internal List<Texture2D> _positiveDeltaTextures;
    protected internal List<Texture2D> _negativeDeltaTextures;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal SimLayer AddWeighted(SimLayer other, float weight)
    {
        Texture2D deltaTexture = new Texture2D(_texture.width, _texture.height);
        
        for (int x = 0; x < _texture.width; x++)
        {
            for (int y = 0; y < _texture.height; y++)
            {
                deltaTexture.SetPixel(x, y, other._texture.GetPixel(x, y) * weight);
            }
        }
        
        _positiveDeltaTextures.Add(deltaTexture);
        return this;
    }
    
    internal SimLayer SubtractWeighted(SimLayer other, float weight)
    {
        Texture2D deltaTexture = new Texture2D(_texture.width, _texture.height);
        
        for (int x = 0; x < _texture.width; x++)
        {
            for (int y = 0; y < _texture.height; y++)
            {
                deltaTexture.SetPixel(x, y, other._texture.GetPixel(x, y) * weight);
            }
        }
        
        _negativeDeltaTextures.Add(deltaTexture);
        return this;
    }

    SimLayer DistributeEvenly(float speed)
    {
        Texture2D deltaTexture = new Texture2D(_texture.width, _texture.height);
        
        for (int x = 0; x < _texture.width; x++)
        {
            for (int y = 0; y < _texture.height; y++)
            {
                float val = _texture.GetPixel(x, y).grayscale;
                float valLeft = 0;
                float valRight = 0;
                float valUp = 0;
                float valDown = 0;
                if (x > 0)
                {
                    valLeft = _texture.GetPixel(x - 1, y).grayscale;
                }

                if (x < _texture.width - 1)
                {
                    valRight = _texture.GetPixel(x + 1, y).grayscale;
                }

                if (y > 0)
                {
                    valDown = _texture.GetPixel(x, y - 1).grayscale;
                }

                if (y < _texture.height - 1)
                {
                    valUp = _texture.GetPixel(x, y + 1).grayscale;
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
        Texture2D deltaTexture = new Texture2D(_texture.width, _texture.height);
        
        for (int x = 0; x < _texture.width; x++)
        {
            for (int y = 0; y < _texture.height; y++)
            {
                float val = _texture.GetPixel(x, y).grayscale;
                float otherVal = other._texture.GetPixel(x, y).grayscale;
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
            for (int x = 0; x < _texture.width; x++)
            {
                for (int y = 0; y < _texture.height; y++)
                {
                    _texture.SetPixel(x, y, _texture.GetPixel(x, y) + deltaTexture.GetPixel(x, y));
                }
            }
        }
        
        foreach (Texture2D deltaTexture in _negativeDeltaTextures)
        {
            for (int x = 0; x < _texture.width; x++)
            {
                for (int y = 0; y < _texture.height; y++)
                {
                    _texture.SetPixel(x, y, _texture.GetPixel(x, y) - deltaTexture.GetPixel(x, y));
                }
            }
        }
    }

    Color GetAverage()
    {
        Color average = new Color(0, 0, 0, 0);
        
        for (int x = 0; x < _texture.width; x++)
        {
            for (int y = 0; y < _texture.height; y++)
            {
                average += _texture.GetPixel(x, y);
            }
        }
        
        average /= _texture.width * _texture.height;
        return average;
    }
}
