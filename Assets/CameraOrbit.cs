using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour {

    public const float MINY = 0.0f;
    public const float MAXY = 50.0f;

    public Transform Target;
    public Transform CameraTransform;

    private float _distance = 10.0f;
    private float _currentX = 0.0f;
    private float _currentY = 0.0f;

    // Use this for initialization
    void Start () {
        if (Target == null)
            Debug.LogError("La camera doit être liée à une cible : Target");

        CameraTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        _currentX += Input.GetAxis("cameraX");
        _currentY += Input.GetAxis("cameraY");

        _currentY = Mathf.Clamp(_currentY, MINY, MAXY);
	}

    void LateUpdate()
    {
        Vector3 direction = new Vector3(0, 0, -_distance);
        Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);
        CameraTransform.position = Target.position + rotation * direction;

        CameraTransform.LookAt(Target.position);
    }
}
