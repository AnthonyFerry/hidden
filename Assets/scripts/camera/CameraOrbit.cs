using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwissArmyKnife;


#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(CameraOrbit))]
public class CameraOrbitEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CameraOrbit myScript = (CameraOrbit)target;
        if (GUILayout.Button("Aiming view"))
        {
            myScript.ChangeCameraView("Aiming");
        }

        if (GUILayout.Button("Walking view"))
        {
            myScript.ChangeCameraView("Walking");
        }

        if (GUILayout.Button("Platform view"))
        {
            myScript.ChangeCameraView("Platform");
        }

        if (GUILayout.Button("Fighting view"))
        {
            myScript.ChangeCameraView("Fighting");
        }
    }
}

#endif


public class CameraOrbit : MonoBehaviour {

    public Transform Target;
    public Transform CameraTransform;
    public CameraSetting CameraSettings;

    [Header("Camera speed")]
    public float VerticalSpeed = 2;
    public float HorizontalSpeed = 3;

    [Header("Recentering parameters")]
    public float TimeBeforeRecenter = 3.0f;
    public float RecenterSpeedX = 3.0f;
    public float RecenterSpeedY = 5.0f;

    public bool recenter { get { return _recenter; } set { _recenter = value; } }
    public float distance { set { _distance = value; } get { return _distance; } }

    private float _time = 0;
    private bool _recenter = false;
    private float _minimumY = 0.0f;
    private float _maximumY = 50.0f;

    private float _distance = 10.0f; // Distance actuelle
    private float _newDistance = 10.0f; // Distance à atteindre
    private float _currentX = 0.0f;
    private float _currentY = 20.0f;

    // Use this for initialization
    void Start () {
        if (Target == null)
            Debug.LogError("A target must be link to this script");

        CameraTransform = transform;

        if (CameraSettings != null)
        {
            var data = GetData("Walking");
            SetCameraParameters(data);  
        }
        else
        {
            Debug.LogWarning("Camera settings has not been found");
            return;
        }
	}

    void FixedUpdate()
    {
        if (GameController.Instance.State != GameState.running)
            return; 

        if (Input.GetButtonDown("recenterCamera"))
            RecenterCamera();

        if (Input.GetAxis("aim") > 0)
            ChangeCameraView("Aiming");
        else
            ChangeCameraView("Walking");
    }

    // Update is called once per frame
	void Update ()
    {
        if (GameController.Instance.State != GameState.running)
            return;
        
        float cameraX = Input.GetAxis("cameraX");
        float cameraY = Input.GetAxis("cameraY");

        // On récupère le composant CharacterController du player
        var controller = Target.GetComponentInParent<CharacterController>();

        if (_newDistance != _distance)
            _distance = Mathf.Lerp(_distance, _newDistance, Time.deltaTime * 10);

        // Si ni la caméra bouge ni le joueur on incrémente le timer
        if (cameraX == 0 && cameraY == 0 && controller.velocity == Vector3.zero)
        {
            _time += Time.deltaTime;
        }
        else
        {
            _time = 0;
            _recenter = false;
        }

        // Si le temps qui s'est écoulé est supérieur ou égale à TimeBeforRecenter on recentre la caméra
        if (_time >= TimeBeforeRecenter)
            _recenter = true;


        if (_recenter)
        {
            _currentX = Mathf.LerpAngle(_currentX, Target.rotation.eulerAngles.y, Time.deltaTime * RecenterSpeedX);
            _currentY = Mathf.LerpAngle(_currentY, 20, Time.deltaTime * RecenterSpeedY);
        }

        _currentX += cameraX * HorizontalSpeed;
        _currentY += cameraY * VerticalSpeed;

        _currentY = Mathf.Clamp(_currentY, _minimumY, _maximumY);
	}

    void LateUpdate()
    {
        if (GameController.Instance.State != GameState.running)
            return;

        Vector3 direction = new Vector3(0, 0, -_distance);
        Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);
        CameraTransform.position = Target.position + rotation * direction;

        CameraTransform.LookAt(Target.position);
    }

    public void RecenterCamera()
    {
        _recenter = true;
    }

    protected CameraData GetData(string settingName)
    {
        var datas = CameraSettings.Data;

        foreach (var setting in datas)
            if (setting.Name == settingName)
                return setting;

        return datas[CameraSetting.WALKING];
    }

    void SetCameraParameters(CameraData data)
    {
        _newDistance = data.Distance;
        _minimumY = data.VerticalMinConstraint;
        _maximumY = data.VerticalMaxConstraint;
    }

    public void ChangeCameraView(string settingName)
    {
        var data = GetData(settingName);
        SetCameraParameters(data);      
    }
}
