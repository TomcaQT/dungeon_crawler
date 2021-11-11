using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    private Vector2 _movement;

    [SerializeField] private float _speed = 10f;

    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update ()
    {
        GetInput();
    }
    
    private void FixedUpdate()
    {
        _rigidbody.velocity = _movement.normalized * _speed;
    }
    
    private void GetInput()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical"); 
    }
    
    

    
}
