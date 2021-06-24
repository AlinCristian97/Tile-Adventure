using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _myRigitBody;

    [SerializeField] private float _runSpeed = 5f;
    
    private void Awake()
    {
        _myRigitBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
    }

    private void Run()
    {
        float controlFlow = Input.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(controlFlow * _runSpeed, _myRigitBody.velocity.y);
        _myRigitBody.velocity = playerVelocity;
    }
}
