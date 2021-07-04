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

    // State
    private bool _isAlive = true;
    
    // Cached component references
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private Collider2D _collider2D;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Run();
        Jump();
        ClimbLadder();
        FlipSprite();
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
        if (!_collider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            return;

        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(_rigidBody2D.velocity.x, controlThrow * _climbSpeed);

        _rigidBody2D.velocity = climbVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(_rigidBody2D.velocity.y) > Mathf.Epsilon;
        _animator.SetBool("IsClimbing", playerHasVerticalSpeed);
    }

    private void Jump()
    {
        if (!_collider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
}