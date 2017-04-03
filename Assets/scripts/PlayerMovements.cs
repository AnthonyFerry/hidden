using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour {

    public float WalkSpeed = 10.0f;
    public float Gravity = 9.0f;
    public float JumpStrength = 100f;

    public PlayerState State = PlayerState.walking;

    private Transform _camera;
    private Vector3 _camForward;
    private Vector3 _move;
    private bool _jump;
    private CharacterController _controller;
    private float _verticalSpeed = 0.0f;

    void Start()
    {
        if (Camera.main != null)
            _camera = Camera.main.transform;
        else
            Debug.LogWarning("Pas de camera trouvée");

        _controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        _verticalSpeed -= Gravity;
    }

    void Update()
    {
        if (_controller.isGrounded)
        {
            _verticalSpeed = 0;
        }

        float horizontal = Input.GetAxis("horizontal");
        float vertical = Input.GetAxis("vertical");

        if (_camera != null)
        {
            _camForward = Vector3.Scale(_camera.forward, new Vector3(1, 0, 1)).normalized;
            _move = vertical * _camForward + horizontal * _camera.right;
        }
        else
        {
            _move = vertical * Vector3.forward + horizontal * Vector3.right;
        }

        if (Input.GetButtonDown("jump"))
            _verticalSpeed = JumpStrength;

        Debug.DrawLine(transform.position, _move * 5 + transform.position);
        transform.LookAt(_move + transform.position);

        _move.y = _verticalSpeed * Time.deltaTime;

        _controller.Move(_move * Time.deltaTime * WalkSpeed );
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Wall")
        {
            var newRotation = Quaternion.LookRotation(-hit.normal);
            transform.rotation = newRotation;
        }
    }
}

public enum PlayerState
{
    walking,
    climbing,
    crouching,
    flying,
    aiming
}