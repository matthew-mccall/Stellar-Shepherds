using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Climate;
using Ecosystem;
using UnityEngine;
using UnityEngine.UIElements;

public class Planet : MonoBehaviour
{
    public double temperatureChange = 0;
    public double averageCarbonUnits = 0;
    public double deltaSeaLevel = 0;
    
    public float orbitalVelocity = 1.0f; // degrees per second
    public float spinRate = 6.0f; // degrees per second
    
    public int hoverOutlineWidth = 3;
    
    public int simLayerWidth = 512;
    public int simLayerHeight = 512;
    
    private Transform _planetTransform;
    private Outline _outline;
    
    private UIDocument _uiDocument;

    private List<SimLayer> _simLayers;
    private List<Material> _simLayerMaterials;
    private List<Label> _simLayerValueLabels;
    
    // Start is called before the first frame update
    void Start()
    {
        _planetTransform = GetComponent<Transform>();
        _outline = GetComponent<Outline>();
        // _uiDocument = GameObject.Find("UI").GetComponent<UIDocument>();
        _uiDocument = GetComponent<UIDocument>();

        _simLayers = new List<SimLayer>();
        _simLayers.Add(new Temperature(simLayerWidth, simLayerHeight));
        _simLayers.Add(new Rainfall(simLayerWidth, simLayerHeight));
        _simLayers.Add(new CarbonPollution(simLayerWidth, simLayerHeight));

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
        
        _simLayerValueLabels = new List<Label>();
        var rootVisualElement = _uiDocument.rootVisualElement;
        
        // Create a row for each sim layer with a label and a value
        foreach (var layer in _simLayers)
        {
            var row = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    alignItems = Align.Center,
                    justifyContent = Justify.SpaceBetween
                }
            };

            var label = new Label(layer.Name)
            {
                style =
                {
                    unityTextAlign = TextAnchor.MiddleLeft,
                    color = Color.white
                }
            };
            row.Add(label);
            
            var value = new Label
            {
                style =
                {
                    unityTextAlign = TextAnchor.MiddleRight,
                    color = Color.white
                },
                name = layer.Name
            };
            // row.Add(value);
            _simLayerValueLabels.Add(value);
            row.Add(_simLayerValueLabels.Last());
            
            rootVisualElement.Add(row);
        }
        
        HideUI();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Orbit around (0, 0, 0)
        _planetTransform.RotateAround(Vector3.zero, Vector3.up, orbitalVelocity * Time.deltaTime);
        
        // Spin on its own axis
        _planetTransform.Rotate(Vector3.up, spinRate * Time.deltaTime);
        
        // Get average value of layers and update labels
        foreach (var layer in _simLayers)
        {
            // get label by name
            var valueLabel = _uiDocument.rootVisualElement.Q<Label>(layer.Name);
            valueLabel.text = layer.GetAverage().ToString(CultureInfo.InvariantCulture);
        }

        if (Input.GetButton("Cancel"))
        {
            HideUI();
        }
    }

    public void ShowUI()
    {
        // show the UI
        _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    public void HideUI()
    {
        // hide the UI
        _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
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
        
        ShowUI();
    }
}
