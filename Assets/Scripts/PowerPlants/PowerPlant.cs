using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerPlant : MonoBehaviour
{
    double chanceOfFailure;
    bool maintained = false;
    

    public double maxPower;
    public double carbonFootprintRadius;
    public double carbonFootprintIntensity;
    public double maintenanceCost;
    public static int initialCost;
    public bool failed = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!maintained) {
            chanceOfFailure += 0.01;
        }

        if (chanceOfFailure > Random.Range(0.0f,1.0f)) {
            this.Fail();
        }
    }

    void Fail() 
    {
        this.failed = true;
        this.maxPower = 0;
    }

    public void Maintain()
    {
        maintained = true;
    }
}
