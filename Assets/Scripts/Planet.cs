using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public double temperature_change = 0;
    public double average_carbon_units = 0;
    public double delta_sea_level = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void carbon_change(double amount)
    {
        average_carbon_units += amount;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    void LateUpdate()
    {
        
    }
}
