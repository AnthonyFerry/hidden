using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardEffect : MonoBehaviour {

    Camera _camera;

	// Use this for initialization
	void Start () {
        _camera = Camera.main;

        if (_camera == null)
            Debug.LogError("Aucune camera");
	}
	
	// Update is called once per frame
	void Update () {
        if (_camera == null)
            return;

        var cameraPosition = _camera.gameObject.transform.position;
        transform.LookAt(cameraPosition);
	}
}
