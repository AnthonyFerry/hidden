using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(MovingFloor))]
public class MovingFloorEditor : Editor
{
    Vector3 NewCoordinates;
    int index;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MovingFloor myScript = (MovingFloor)target;


        EditorGUILayout.LabelField("Modification des points", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginVertical("box");
                GUILayout.Label("Ajouter");

                NewCoordinates = EditorGUILayout.Vector3Field("Number of new objects", NewCoordinates);
                if (GUILayout.Button("Add point"))
                    myScript.AddDestinationPoint(NewCoordinates);

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box");
                GUILayout.Label("Supprimer");

                index = EditorGUILayout.IntField("Index", index);
                index = Mathf.Clamp(index, 0, myScript.PointCount);

                if (GUILayout.Button("Destroy at index"))
                    myScript.DestroyPoint(index);

                if (GUILayout.Button("Destroy last point"))
                        myScript.DestroyLastPoint();

                if (GUILayout.Button("Destroy all"))
                    myScript.DestroyAll();

            EditorGUILayout.EndVertical();

        if (GUILayout.Button("Refresh list"))
            myScript.ReloadPointToList();

        EditorGUILayout.EndVertical();
    }
}

#endif

public class MovingFloor : TriggerableObject {

    /// <summary>
    /// GameObject contenant les points de destinations
    /// </summary>
    public Transform Container;

    /// <summary>
    /// Plateforme ou objet à déplacer
    /// </summary>
    public Transform Platform;

    public float Speed = 10;

    /// <summary>
    /// Liste des points de destination
    /// </summary>
    public List<GameObject> DestinationPoints;
    public int PointCount {  get { return DestinationPoints.Count; } }
    public bool StopBetweenTwoPoints = false;
    public float TimeToWait = 2;
    private float _waitingTime = 0;


    int _pointToReach = 1;

    #region Triggers
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
            col.gameObject.transform.SetParent(transform);
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")
            col.gameObject.transform.SetParent(null);
    }
    #endregion

    #region Management
    public void AddDestinationPoint(Vector3 newCoord)
    {
        var go = new GameObject("Destination" + PointCount);
        go.transform.SetParent(Container);
        go.transform.position = newCoord;
        DestinationPoints.Add(go);
    }

    public void DestroyPoint(int index)
    {
        if (PointCount < 1)
            return;

        var pointToDestroy = DestinationPoints[index];
        if (pointToDestroy != null)
        {
            DestroyImmediate(pointToDestroy);
            DestinationPoints.Remove(pointToDestroy);
        }
    }

    public void DestroyLastPoint()
    {
        if (PointCount > 0)
        {
            var pointToDestroy = DestinationPoints[PointCount - 1];
            DestroyImmediate(pointToDestroy);
            DestinationPoints.Remove(pointToDestroy);
        }
            
    }

    public void DestroyAll()
    {
        for (int i = 0; i < PointCount; i++)
        {
            var pointToDestroy = DestinationPoints[i];
            if (pointToDestroy != null)
            {
                DestroyImmediate(pointToDestroy);
            }
        }

        DestinationPoints.Clear();
    }

    public void ReloadPointToList()
    {
        DestinationPoints.Clear();

        int pointCount = Container.childCount;

        for (int i = 0; i < pointCount; i++)
            DestinationPoints.Add(Container.GetChild(i).gameObject);
    }

    #endregion

    public override void Activate()
    {
        IsActive = true;
    }

    void Start()
    {
        ReloadPointToList();

        if (DestinationPoints.Count < 2)
        {
            Debug.LogError("Le script moving floor doit avoir au moins deux points de destination");
            return;
        }

        Platform.position = DestinationPoints[0].transform.position;
    }

    void Update()
    {
        if (DestinationPoints.Count < 2)
            return;

        if (!IsActive)
        {
            return;
        }

        var step = Time.deltaTime * Speed;
        var pos = Platform.position;
        var pointToReach = DestinationPoints[_pointToReach].transform.position;
        Platform.position = Vector3.MoveTowards(pos, pointToReach, step);

        if (pos == pointToReach)
        {
            if (StopBetweenTwoPoints)
            {
                _waitingTime += Time.deltaTime;

                if (_waitingTime >= TimeToWait)
                {
                    _waitingTime = 0;
                    GoToNextPoint();
                }
            }
            else
            {
                GoToNextPoint();
            }
        }
    }

    void GoToNextPoint()
    {
        var next = _pointToReach + 1;
        if (next > PointCount - 1)
        {
            next = 0;
        }

        _pointToReach = next;
    }
}
