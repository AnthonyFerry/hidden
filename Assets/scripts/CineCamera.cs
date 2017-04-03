using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineCamera : MonoBehaviour {

    public GameObject _camera;
    public GameObject target;

    private bool _go;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (_go)
        {
            _camera.GetComponent<CameraOrbit>().enabled = false;
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, target.transform.position, Time.deltaTime * 5);
            _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, target.transform.rotation, Time.deltaTime * 5);
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "Player")
        {
            _go = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.name == "Player")
        {
            _go = false;

            _camera.GetComponent<CameraOrbit>().enabled = true;
            _camera.GetComponent<CameraOrbit>().distance = Vector3.Distance(FindObjectOfType<PlayerResources>().transform.position, _camera.transform.position);

            

        }
    }
}
