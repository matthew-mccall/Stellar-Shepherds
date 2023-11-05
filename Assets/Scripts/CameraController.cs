using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int moveSpeed = 10;
    public int zoomSpeed = 1000;
    public float minZoom = 2;
    public float maxZoom = 100;
    
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        var cameraTransform = transform;
        _initialPosition = cameraTransform.position;
        _initialRotation = cameraTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        var cameraTransform = transform;
        var parentPosition = cameraTransform.parent ? cameraTransform.parent.position : Vector3.zero;
        
        if (Input.GetButton("Fire2"))
        {
            cameraTransform.RotateAround(parentPosition, Vector3.up, Input.GetAxis("Mouse X") * moveSpeed);
        }
        
        // Zoom in/out
        cameraTransform.Translate(Vector3.forward * (Input.GetAxis("Mouse ScrollWheel") * zoomSpeed));
        
        if (Input.GetButton("Cancel"))
        {
            // Reset camera parent
            cameraTransform.parent = null;
            
            // Reset camera position
            cameraTransform.position = _initialPosition;
            cameraTransform.rotation = _initialRotation;
        }
    }
}
