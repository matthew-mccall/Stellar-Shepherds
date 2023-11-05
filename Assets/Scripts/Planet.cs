using System;
using System.Collections;
using System.Collections.Generic;
using Climate;
using Ecosystem;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public double temperatureChange = 0;
    public double averageCarbonUnits = 0;
    public double deltaSeaLevel = 0;
    
    public float orbitalVelocity = 1.0f; // degrees per second
    public float spinRate = 6.0f; // degrees per second
    
    public int hoverOutlineWidth = 3;
    
    private Transform _planetTransform;
    private Outline _outline;

    private List<SimLayer> _simLayers;
    private List<Material> _simLayerMaterials;
    
    // Start is called before the first frame update
    void Start()
    {
        _planetTransform = GetComponent<Transform>();
        _outline = GetComponent<Outline>();
        
        _simLayers = new List<SimLayer>();
        
        // Make materials for each sim layer
        _simLayerMaterials = new List<Material>();
        foreach (var simLayer in _simLayers)
        {
            var material = new Material(Shader.Find("Universal Render Pipeline/Lit"))
            {
                mainTexture = simLayer.Texture
            };
            _simLayerMaterials.Add(material);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // Orbit around (0, 0, 0)
        _planetTransform.RotateAround(Vector3.zero, Vector3.up, orbitalVelocity * Time.deltaTime);
        
        // Spin on its own axis
        _planetTransform.Rotate(Vector3.up, spinRate * Time.deltaTime);
    }

    private void OnMouseOver()
    {
        if (Camera.main != null && Camera.main.GetComponent<CameraController>().targetTransform == _planetTransform) return;
        
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
        var cameraController = Camera.main.GetComponent<CameraController>();
        
        if (cameraController.targetTransform == _planetTransform) return;
        
        cameraController.targetTransform = _planetTransform;
        cameraController.SetDistanceFrom(_planetTransform.position, 600);
    }
}
