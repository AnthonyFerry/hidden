using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public Transform LookAt;
    public Transform CameraTransform;

    private Camera _camera;

    private const float WALKING_DISTANCE = 1.0f;
    private const float PLATFORM_DISTANCE = 1.0f;
    private const float AIMING_DISTANCE = 1.0f;
    private const float FIGHTING_DISTANCE = 1.0f;

    private float _distance = 10.0f;
    private float _currentX = 0.0f;
    private float _currentY = 0.0f;
    private float _sensivityX = 4.0f;
    private float _sensivityY = 1.0f;

    private float _minY = -20f, _maxY = 50f;

    private void Start()
    {
        CameraTransform = transform;
        _camera = Camera.main;
    }

    private void Update()
    {
        _currentX += Input.GetAxis("cameraX");
        _currentY += Input.GetAxis("cameraY");

        _currentY = Mathf.Clamp(_currentY, _minY, _maxY);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -_distance);
        Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);
        CameraTransform.position = LookAt.position + rotation * dir;
        CameraTransform.LookAt(LookAt.position);
    }
}

public enum CameraStyle
{
    walking,
    fighting,
    platform,
    aiming
};