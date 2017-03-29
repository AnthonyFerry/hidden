using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraSetting", menuName = "Hidden", order = 1)]
public class CameraSetting : ScriptableObject { public List<CameraData> Data; }

[System.Serializable]
public struct CameraData
{
    public string Name;
    public float Distance;
    public float VerticalMinConstraint;
    public float VerticalMaxConstraint;
}