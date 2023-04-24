using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
    private bool _isAttacking;
    
    [Header("Move info")] 
    [SerializeField] private float moveSpeed;

    [Header("Player detection")] 
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D _isPlayerDetected;
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (_isPlayerDetected)
        {
            if (_isPlayerDetected.distance > 1)
            {
                _rb.velocity = new Vector2(moveSpeed * 1.5f * _facingDir, _rb.velocity.y);
                
                Debug.Log("I see the player");
                _isAttacking = false; 
            }
            else
            {
                Debug.Log("ATTACK! " + _isPlayerDetected.collider.gameObject.name);
                _isAttacking = true;
            }
        }
            

        if(!_isGrounded || _isWallDetected)
            Flip();
        
        Movement();
    }
    
    private void Movement()
    {
        if(!_isAttacking)
            _rb.velocity = new Vector2(moveSpeed * _facingDir, _rb.velocity.y);
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();

        _isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * _facingDir,
            whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * _facingDir,transform.position.y));
    }
}
