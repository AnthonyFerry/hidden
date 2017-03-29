using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(MovingFloor))]
public class MovingFloorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MovingFloor myScript = (MovingFloor)target;

        if (GUILayout.Button("Add a destination point"))
            myScript.AddDestinationPoint();
    }
}

#endif

public class MovingFloor : MonoBehaviour {

    public List<GameObject> DestinationPoints;

	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            col.gameObject.transform.SetParent(transform);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            col.gameObject.transform.SetParent(null);
        }
    }

    public void AddDestinationPoint()
    {

    }

    void Update()
    {
        foreach (var point in DestinationPoints)
        {
        }
    }
}
