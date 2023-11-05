using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int moveSpeed = 10;
    public int zoomSpeed = 1000;

    public Transform targetTransform;
    
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private readonly uint _initialDistanceFromSun = 10000;

    public Vector3 lastOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        var cameraTransform = transform;
        var cameraPosition = cameraTransform.position;

        _initialPosition = cameraPosition;
        _initialRotation = cameraTransform.rotation;
        
        SetDistanceFrom(Vector3.zero, _initialDistanceFromSun);
        cameraTransform.RotateAround(Vector3.zero, Vector3.up, 45);
        
        lastOffset = cameraPosition - cameraPosition;
    }

    // Update is called once per frame
    void Update()
    {
        var cameraTransform = transform;
        var targetPosition = targetTransform != null ? targetTransform.position : Vector3.zero;
        
        cameraTransform.position = targetPosition + lastOffset;
        
        if (Input.GetButton("Fire2"))
        {
            cameraTransform.RotateAround(targetPosition, Vector3.up, Input.GetAxis("Mouse X") * moveSpeed);
        }
        
        // Zoom in/out
        cameraTransform.Translate(Vector3.forward * (Input.GetAxis("Mouse ScrollWheel") * zoomSpeed));
        lastOffset = cameraTransform.position - targetPosition;
        
        if (Input.GetButton("Cancel"))
        {
            SetDistanceFrom(Vector3.zero, _initialDistanceFromSun);
            targetTransform = null;
        }
    }
    
    public void SetDistanceFrom(Vector3 position, uint distance)
    {
        var cameraTransform = transform;
        
        cameraTransform.position = position;
        cameraTransform.Translate(0, 0, -distance);
        
        lastOffset = cameraTransform.position - position;
    }
}
