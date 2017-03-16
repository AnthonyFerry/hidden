using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Camera Settings", menuName = "Hidden", order = 1)]
public class CameraSettings : ScriptableObject {

    public CameraData Data;

}

[System.Serializable]
public struct CameraData
{
    public float distance;
    public float minConstraint;
    public float maxConstraint;
}