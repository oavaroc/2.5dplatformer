using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private PlayerInputActions _input;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _gravity = 1f;
    [SerializeField] private float _jumpHeight = 15f;


    private float _yVelocity = 0f;

    [SerializeField]
    private int _maxJumps = 2;
    private int _jumpsPerformed=0;

    private bool _wallJumping=false;
    private Vector3 _wallJumpDirection=Vector3.zero;

    private Vector3 _movement = Vector3.zero;

    private float _horizontalInput = 0;

    [SerializeField]
    private float _pushPower = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Character controller is null");
        }
        _input = new PlayerInputActions();
        _input.Player.Enable();
        UIManager.Instance.UpdateLives(3);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void FixedUpdate()
    {

        if(_controller.isGrounded)
        {
            _jumpsPerformed = 0;

        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!_controller.isGrounded && hit.transform.CompareTag("Wall"))
        {
            _wallJumping = true;
            _wallJumpDirection = hit.normal;
            Debug.DrawRay(hit.point,hit.normal,Color.blue);
        }
        else if (hit.transform.CompareTag("Box"))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb == null || rb.isKinematic || hit.moveDirection.y < -0.3f)
            {
                return;
            }
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);
            rb.velocity = pushDir * _pushPower;
        }
    }
    
    

    private void HandleMovement()
    {
        _horizontalInput = _input.Player.HorizontalMovement.ReadValue<float>();
        if (_controller.isGrounded)
        {
            _movement = new Vector3(_horizontalInput, 0, 0) * _speed;
            _wallJumping = false;
        }
        else
        {
            _yVelocity -= _gravity;
        }
        HandleJump();
        _movement.y = _yVelocity;
        _controller.Move(_movement * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (_input.Player.Jump.WasPerformedThisFrame() && _jumpsPerformed < _maxJumps)
        {
            if (_wallJumping)
            {
                _movement = _wallJumpDirection * _speed;
                _jumpsPerformed = 0;
                _wallJumping = false;
            }
            else
            {
                _movement = new Vector3(_horizontalInput, 0, 0) * _speed;
                _jumpsPerformed++;
            }
            _yVelocity = _jumpHeight;
        }

    }


}