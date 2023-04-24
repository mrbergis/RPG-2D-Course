using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Dash info")] 
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;
    
    private float _xInput;
    
    private int _facingDir = 1;
    private bool _facingRight = true;

    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatsIsGround;
    private bool _isGrounded;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();

    }
        
    void Update()
    {   
        Movement(); 
        CheckInput();
        CollisionChecks();

        dashTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashTime = dashDuration;
        }
        
        FlipController();
        AnimatorControllers();
    }

    private void CollisionChecks()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down,groundCheckDistance, whatsIsGround); 
    }
    
    private void CheckInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Movement()
    {
        if (dashTime > 0)
        {
            _rb.velocity = new Vector2(_xInput * dashSpeed, 0);
        }
        else
        {
            _rb.velocity = new Vector2(_xInput * moveSpeed, _rb.velocity.y);
        }
    }

    private void Jump()
    {
        if(_isGrounded)
            _rb.velocity = new Vector2(_rb.velocity.x,jumpForce);
    }
    
    private void AnimatorControllers()
    {
        bool isMoving = _rb.velocity.x != 0;
        
        _anim.SetFloat("yVelocity", _rb.velocity.y);
        
        _anim.SetBool("isMoving", isMoving);
        _anim.SetBool("isGrounded", _isGrounded);
        _anim.SetBool("isDashing", dashTime > 0);
    }

    private void Flip()
    {
        _facingDir = _facingDir * -1;
        _facingRight = !_facingRight;
        transform.Rotate(0,180,0);
    }

    private void FlipController()
    {
        if (_rb.velocity.x > 0 && !_facingRight)
            Flip();
        else if (_rb.velocity.x < 0 && _facingRight)
            Flip();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
