using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] private float _runSpeed = 5f;
    [SerializeField] private float _jumpSpeed = 5f;
    [SerializeField] private float _climbSpeed = 5f;
    [SerializeField] private Vector2 _deathKick = new Vector2(25f, 25f);

    // State
    private bool _isAlive = true;
    
    // Cached references
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private CapsuleCollider2D _bodyCollider2D;
    private BoxCollider2D _feetCollider2D;
    
    private float _startGravityScale;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _bodyCollider2D = GetComponent<CapsuleCollider2D>();
        _feetCollider2D = GetComponent<BoxCollider2D>();
        
        _startGravityScale = _rigidBody2D.gravityScale;
    }

    private void Update()
    {
        if (!_isAlive) return;
        
        Run();
        ClimbLadder();
        Jump();
        FlipSprite();
        Die();
    }

    private void Run()
    {
        float controlFlow = Input.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(controlFlow * _runSpeed, _rigidBody2D.velocity.y);
        _rigidBody2D.velocity = playerVelocity;
        
        bool playerHasHorizontalSpeed = Mathf.Abs(_rigidBody2D.velocity.x) > Mathf.Epsilon;
        _animator.SetBool("IsRunning", playerHasHorizontalSpeed);
    }

    private void ClimbLadder()
    {
        if (!_feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            _animator.SetBool("IsClimbing", false);
            _rigidBody2D.gravityScale = _startGravityScale;
            return;
        }

        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(_rigidBody2D.velocity.x, controlThrow * _climbSpeed);
        _rigidBody2D.velocity = climbVelocity;
        _rigidBody2D.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(_rigidBody2D.velocity.y) > Mathf.Epsilon;
        _animator.SetBool("IsClimbing", playerHasVerticalSpeed);
    }

    private void Jump()
    {
        if (!_feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return;

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, _jumpSpeed);
            _rigidBody2D.velocity += jumpVelocityToAdd;
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_rigidBody2D.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rigidBody2D.velocity.x), 1f);
        }
    }

    private void Die()
    {
        if (_bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Obstacles")))
        {
            _isAlive = false;
            _animator.SetTrigger("Die");
            _rigidBody2D.velocity = _deathKick;
            
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}