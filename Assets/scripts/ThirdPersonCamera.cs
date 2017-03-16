using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
[CustomEditor(typeof(ThirdPersonCamera))]
public class ThirdPersonCameraEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ThirdPersonCamera camera = (ThirdPersonCamera)target;

        if (GUILayout.Button("Walking View"))
        {
            camera.ChangeCamera(CameraStyle.walking);
        }

        if (GUILayout.Button("Platform View"))
        {
            camera.ChangeCamera(CameraStyle.platform);
        }

        if (GUILayout.Button("Aiming View"))
        {
            camera.ChangeCamera(CameraStyle.aiming);
        }

        if (GUILayout.Button("Fighting View"))
        {
            camera.ChangeCamera(CameraStyle.fighting);
        }
    }
}

#endif

public class ThirdPersonCamera : MonoBehaviour {

    public Transform LookAt;
    public Transform CameraTransform;

    [Header("Camera Settings")]
    public CameraSettings Walking;
    public CameraSettings Platform;
    public CameraSettings Aiming;
    public CameraSettings Fighting;

    private Camera _camera;

    private float _curDistance = 10.0f;
    private float _newDistance = 20f;
    private float _currentX = 0.0f;
    private float _currentY = 0.0f;
    private float _sensivityX = 4.0f;
    private float _sensivityY = 1.0f;

    private float _minY = -20f, _maxY = 50f;

    private CameraStyle Style;

    private void Start()
    {
        CameraTransform = transform;
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_curDistance != _newDistance)
        {
            _curDistance = Mathf.Lerp(_curDistance, _newDistance, Time.deltaTime * 10);            
        }

        _currentX += Input.GetAxis("cameraX");
        _currentY += Input.GetAxis("cameraY");

        _currentY = Mathf.Clamp(_currentY, _minY, _maxY);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -_curDistance);
        Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);
        CameraTransform.position = LookAt.position + rotation * dir;
        CameraTransform.LookAt(LookAt.position);
    }

    public void ChangeCamera(CameraStyle newStyle)
    {
        CameraSettings camData = Walking;

        switch (newStyle)
        {
            case CameraStyle.walking:
                camData = Walking;
                break;
            case CameraStyle.platform:
                camData = Platform;
                break;
            case CameraStyle.aiming:
                camData = Aiming;
                break;
            case CameraStyle.fighting:
                camData = Fighting;
                break;
        }

        _minY = camData.Data.minConstraint;
        _maxY = camData.Data.maxConstraint;

        _newDistance = camData.Data.distance;

        Style = newStyle;

    }
}

public enum CameraStyle
{
    walking,
    fighting,
    platform,
    aiming
};