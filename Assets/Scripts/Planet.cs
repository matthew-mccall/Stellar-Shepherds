using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public double temperatureChange = 0;
    public double averageCarbonUnits = 0;
    public double deltaSeaLevel = 0;
    
    public float orbitalVelocity = 1.0f; // degrees per second
    
    Transform _planetTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        temperatureChange = 0;
        averageCarbonUnits = 0;
        deltaSeaLevel = 0;
        _planetTransform = GetComponent<Transform>();
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
        // Orbit around (0, 0, 0)
        _planetTransform.RotateAround(Vector3.zero, Vector3.up, orbitalVelocity * Time.deltaTime);
        calculateNewSeaLevel();
    }
    
    void LateUpdate()
    {
        calculateTemperatureChange();
    }
}
