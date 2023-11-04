using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : Species
{
    private List<Prey> preyList;

    public double growthFactor;

    public int dx;

    public int dy;

    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        GenerateDensityMap(dx, dy, color);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
