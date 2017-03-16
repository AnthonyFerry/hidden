using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Parametres personnage")]
    public int Life = 10;
    public float XP = 0;
    public PlayerState State = PlayerState.standing;

    [Header("Parametres vitesse")]
    public float WalkSpeed = 6.0F;
    public float JumpSpeed = 8.0F;
    public float ClimbingSpeedDivider = 0.5F;
    public float CrouchingSpeedDivider = 0.5F;
    public float Gravity = 20.0F;
    public int MaximumJumpCount = 1;

    [Header("Camera")]
    public GameObject Camera;
    
    private Vector3 _moveDirection = Vector3.zero;
    private float _verticalSpeed = 0;

    private int _jumpCount = 0;
    private bool _isCrouching = false;
    private bool _isFlying = false;
    private bool _isFalling = false;

    void FixedUpdate()
    {
        // Si le joueur n'est pas en train de grimper, il est soumis à la gravité
        if (State != PlayerState.climbing)
            _verticalSpeed -= Gravity * Time.deltaTime;
        else
            _verticalSpeed -= 0;
    }

    void Update()
    {
        // Référence vers le composant CharacterController de l'objet
        CharacterController controller = GetComponent<CharacterController>();

        // Si le joueur à touché le sol
        if (controller.isGrounded)
        {
            if (State == PlayerState.climbing)
                State = PlayerState.standing;

            _jumpCount = 0;
            _verticalSpeed = 0;
        }

        MovePlayer();

        if (Input.GetButtonDown("jump"))
        {
            if (_jumpCount < MaximumJumpCount)
            {
                _verticalSpeed = JumpSpeed;
                _jumpCount++;
            }
        }

        if (Input.GetButton("jump") && controller.velocity.y < -0.1 && _jumpCount > 0)
        {
            _verticalSpeed *= 0.8f;
        }

        _moveDirection.y += _verticalSpeed;

        controller.Move(_moveDirection * Time.deltaTime);
    }

    void MovePlayer()
    {
        var horizontal = Input.GetAxis("horizontal");
        var vertical = Input.GetAxis("vertical");

        if (State == PlayerState.standing)
        {
            _moveDirection = new Vector3(horizontal, 0, vertical);
            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= WalkSpeed;

            if (_moveDirection.magnitude != 0)
                CalibrateToCameraRotation();
        }
        else if (State == PlayerState.climbing)
        { 
            _moveDirection = new Vector3(horizontal, vertical, 0);
            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= WalkSpeed * ClimbingSpeedDivider;
        }
        else if (State == PlayerState.crouching)
        {
            _moveDirection = new Vector3(horizontal, 0, vertical);
            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= WalkSpeed * CrouchingSpeedDivider;

            CalibrateToCameraRotation();
        }
    }

    void CalibrateToCameraRotation()
    {
        if (Camera == null)
            return;

        transform.rotation = Quaternion.Euler(0, Camera.transform.localEulerAngles.y, 0);
    }

    #region Triggers
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "ClimbWall")
        {
            _verticalSpeed = 0;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "ClimbWall")
        {
            State = PlayerState.climbing;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "ClimbWall")
        {
            State = PlayerState.standing;
        }
    }

    #endregion
}

public enum PlayerState
{
    standing,
    climbing,
    crouching
}