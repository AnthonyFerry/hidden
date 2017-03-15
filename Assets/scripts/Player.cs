using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int Life = 10;
    public float XP = 0;

    public float WalkSpeed = 6.0F;
    public float JumpSpeed = 8.0F;
    public float Gravity = 20.0F;
    public int MaximumJumpCount = 1;

    public float minimumY = 0f;
    public float maximumY = 0f;
    
    private Vector3 _moveDirection = Vector3.zero;
    private float _verticalSpeed = 0;

    private int _jumpCount = 0;
    private bool _isCrouching = false;
    private bool _isFlying = false;
    private bool _isFalling = false;

    void Start()
    {
    }

    void FixedUpdate()
    {
        _verticalSpeed -= Gravity * Time.deltaTime;
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        

        if (controller.isGrounded)
        {
            _jumpCount = 0;
            _verticalSpeed = 0;
        }

        _moveDirection = new Vector3(Input.GetAxis("horizontal"), 0, Input.GetAxis("vertical"));
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection.x *= WalkSpeed;
        _moveDirection.z *= WalkSpeed;

        if (Input.GetButtonDown("jump"))
            if (_jumpCount < MaximumJumpCount)
            {
                _verticalSpeed = JumpSpeed;
                _jumpCount++;
            }

        if (Input.GetButton("jump") && controller.velocity.y < -0.1 && _jumpCount > 0)
        {
            _verticalSpeed *= 0.8f;
        }

        _moveDirection.y += _verticalSpeed;



        controller.Move(_moveDirection * Time.deltaTime);
    }

    void LateUpdate()
    {
    }
}
