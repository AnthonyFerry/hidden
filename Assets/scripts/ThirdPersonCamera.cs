﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public GameObject Target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(Input.GetAxis("cameraX"), 0, Input.GetAxis("cameraY"));
	}
}
