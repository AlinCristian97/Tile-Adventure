using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1f;
    
    private Rigidbody2D _rigidBody2D;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsFacingRight())
        {
            _rigidBody2D.velocity = new Vector2(_movementSpeed, 0f);
        }
        else
        {
            _rigidBody2D.velocity = new Vector2(-(_movementSpeed), 0f);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(_rigidBody2D.velocity.x)), 1f);
    }
}