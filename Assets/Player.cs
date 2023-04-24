using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Move info")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Dash info")] 
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;

    [SerializeField] private float dashCooldown;
    private float _dashCooldownTimer;

    [Header("Attack info")] 
    [SerializeField]private float comboTime = .3f;
    private float _comboTimeWindow;
    private bool _isAttacking;
    private int _comboCounter;

    private float _xInput;
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {   
        base.Update();
        
        Movement(); 
        CheckInput();

        dashTime -= Time.deltaTime;
        _dashCooldownTimer -= Time.deltaTime;
        _comboTimeWindow -= Time.deltaTime;
        
        FlipController();
        AnimatorControllers();
    }

    public void AttackOver()
    {
        _isAttacking = false;

        _comboCounter++;

        if (_comboCounter > 2)
            _comboCounter = 0;
    }
    
    private void CheckInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }

    private void StartAttackEvent()
    {
        if(!_isGrounded)
            return;
        
        if (_comboTimeWindow < 0)
            _comboCounter = 0;
             
        _isAttacking = true;
        _comboTimeWindow = comboTime;
    }

    private void DashAbility()
    {
        if (_dashCooldownTimer < 0 && !_isAttacking)
        {
            _dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
    }

    private void Movement()
    {
        if (_isAttacking)
        {
            _rb.velocity = new Vector2(0, 0);
        }
        else if (dashTime > 0)
        {
            _rb.velocity = new Vector2(_facingDir * dashSpeed, 0);
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
        _anim.SetBool("isAttacking", _isAttacking);
        _anim.SetInteger("comboCounter", _comboCounter);
    }



    private void FlipController()
    {
        if (_rb.velocity.x > 0 && !_facingRight)
            Flip();
        else if (_rb.velocity.x < 0 && _facingRight)
            Flip();
    }
    
}
