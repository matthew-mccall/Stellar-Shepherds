using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public double temperatureChange = 0;
    public double averageCarbonUnits = 0;
    public double deltaSeaLevel = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        temperatureChange = 0;
        averageCarbonUnits = 0;
        deltaSeaLevel = 0;
    }

    public void carbonChange(double amount)
    {
        averageCarbonUnits += amount;
        return;
    }

    public double getCarbonChange()
    {
        return averageCarbonUnits;
    }

    public double getTemperatureChange()
    {
        return temperatureChange;
    }

    private void calculateTemperatureChange()
    {
        temperatureChange = averageCarbonUnits / 1E6;
    }

    private void calculateNewSeaLevel()
    {
        deltaSeaLevel = averageCarbonUnits / 1E6;
    }
    
    // Update is called once per frame
    void Update()
    {
        calculateNewSeaLevel();
    }
    
    void LateUpdate()
    {
        calculateTemperatureChange();
    }
}
