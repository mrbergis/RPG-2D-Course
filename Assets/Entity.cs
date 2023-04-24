using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D _rb;
    protected Animator _anim;
    
    protected int _facingDir = 1;
    protected bool _facingRight = true;

    [Header("Collision info")] 
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [Space]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatsIsGround;

    protected bool _isGrounded;
    protected bool _isWallDetected;
    
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();

        if (wallCheck == null)
            wallCheck = transform;
    }
    
    protected virtual void Update()
    {
        CollisionChecks();
        
        
    }

    protected virtual void CollisionChecks()
    {
        _isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatsIsGround);
        _isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * _facingDir, whatsIsGround);
    }
    
    protected virtual void Flip()
    {
        _facingDir = _facingDir * -1;
        _facingRight = !_facingRight;
        transform.Rotate(0,180,0);
    }
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * _facingDir, wallCheck.position.y));
    }
}
