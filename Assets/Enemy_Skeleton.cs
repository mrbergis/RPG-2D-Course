using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
    [Header("Move info")] 
    [SerializeField] private float moveSpeed;
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if(!_isGrounded || _isWallDetected)
            Flip();
        
        _rb.velocity = new Vector2(moveSpeed * _facingDir, _rb.velocity.y);
    }
}
