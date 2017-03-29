using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraView : MonoBehaviour {

    public CameraStyle Type;
    public CameraOrbit CameraScript;

	// Use this for initialization
	void Start () {
        CameraScript = FindObjectOfType<CameraOrbit>();
	}
	
	void OnTriggerEnter(Collider obj)
    {
        if (obj.name == "Player")
        {
            string cameraSettings = "Walking";

            switch (Type)
            {
                case CameraStyle.walking:
                    cameraSettings = "Walking";
                    break;
                case CameraStyle.platform:
                    cameraSettings = "Platform";
                    break;
                case CameraStyle.fighting:
                    cameraSettings = "Fighting";
                    break;
            }

            CameraScript.ChangeCameraView(cameraSettings);
        }
    }
}

public enum CameraStyle
{
    walking,
    platform,
    fighting
}