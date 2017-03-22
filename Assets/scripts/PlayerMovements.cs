using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour {

    public Transform Camera;

    public float Speed = 5;
    public float Gravity = 9;
    public float JumpStrength = 10;

    private float _verticalSpeed = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void FixedUpdate()
    {
        _verticalSpeed -= Gravity * Time.deltaTime;
    }

    void Move()
    {
        var horizontal = Input.GetAxis("horizontal");
        var vertical = Input.GetAxis("vertical");
        var characterController = GetComponent<CharacterController>();

        

        transform.forward = newForward;



        /*
        var localPosition = transform.localPosition;
        var newX = localPosition.x + horizontal ;
        var newY = localPosition.y;
        var newZ = localPosition.z + vertical;

        Vector3 direction = new Vector3(newX, newY, newZ);
        

        transform.LookAt(direction);

        if (Input.GetButtonDown("jump"))
            _verticalSpeed = JumpStrength * Time.deltaTime;
            
        direction.y += _verticalSpeed;
        
        characterController.Move((direction - transform.position) * Time.deltaTime * Speed);
        */
    }
}
