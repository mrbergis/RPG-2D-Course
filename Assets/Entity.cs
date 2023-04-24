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
    [SerializeField] protected LayerMask whatsIsGround;
    
    protected bool _isGrounded;
    
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
    }
    
    protected virtual void Update()
    {
        CollisionChecks();
    }

    protected virtual void CollisionChecks()
    {
        _isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down,groundCheckDistance, whatsIsGround); 
    }
    
    protected virtual void Flip()
    {
        _facingDir = _facingDir * -1;
        _facingRight = !_facingRight;
        transform.Rotate(0,180,0);
    }
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
