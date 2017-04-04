using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingPoint : MonoBehaviour {

    public bool IsActive = false;

    private Light _light;

	// Use this for initialization
	void Start () {
        _light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if (IsActive)
            if (_light.color == Color.cyan)
                return;


	}

    void OnTriggerEnter(Collider col)
    { }

    
}
