using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public double temperature_change = 0;
    public double average_carbon_units = 0;
    public double delta_sea_level = 0;
    
    public float orbitalVelocity = 1.0f; // degrees per second
    public GameObject mainCamera;
    
    private Transform _planetTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        _planetTransform = GetComponent<Transform>();
    }

    public void carbon_change(double amount)
    {
        average_carbon_units += amount;
    }
    
    // Update is called once per frame
    void Update()
    {
        // Orbit around (0, 0, 0)
        _planetTransform.RotateAround(Vector3.zero, Vector3.up, orbitalVelocity * Time.deltaTime);
    }
    
    void LateUpdate()
    {
        
    }

    private void OnMouseOver()
    {
        // outline the planet
    }

    private void OnMouseExit()
    {
        // remove outline
    }

    private void OnMouseDown()
    {
        // zoom to planet
        
    }
}
