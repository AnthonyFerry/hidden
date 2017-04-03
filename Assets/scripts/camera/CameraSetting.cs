using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraSetting", menuName = "Hidden", order = 1)]
public class CameraSetting : ScriptableObject {
    public List<CameraData> Data;
    public const int AIMING = 0;
    public const int WALKING = 1;
    public const int PLATFORM = 1;
    public const int FIGHTING = 1;
}

[System.Serializable]
public struct CameraData
{
    public string Name;
    public float Distance;
    public float VerticalMinConstraint;
    public float VerticalMaxConstraint;
}