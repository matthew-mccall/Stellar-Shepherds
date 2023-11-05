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
    
    public int hoverOutlineWidth = 3;
    public Vector3 cameraOffset = new Vector3(0, 0, -400);
    
    private Transform _planetTransform;
    private Outline _outline;
    
    // Start is called before the first frame update
    void Start()
    {
        _planetTransform = GetComponent<Transform>();
        _outline = GetComponent<Outline>();
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
        if (Camera.main == null) return;
        if (Camera.main.transform.parent == _planetTransform) return;
        
        // outline the planet
        _outline.OutlineWidth = hoverOutlineWidth;
    }

    private void OnMouseExit()
    {
        // remove outline
        _outline.OutlineWidth = 0;
    }

    private void OnMouseDown()
    {
        if (Camera.main == null) return;
        
        var cameraTransform = Camera.main.transform;
        
        cameraTransform.SetParent(_planetTransform);
        cameraTransform.localPosition = cameraOffset;
        cameraTransform.LookAt(_planetTransform);
    }
}
