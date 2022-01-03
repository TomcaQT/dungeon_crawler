using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxLifetime = 5f;

    [SerializeField] private float _damage = 10f;
    
    
    private void Start()
    {
        Destroy(gameObject,_maxLifetime);
    }

    public void Initialize(float damage = 10f, float speed = 10f, float maxLifetime = 5f)
    {
        _damage = damage;
        _speed = speed;
        _maxLifetime = maxLifetime;
    }
    
    public void Shoot(Vector3 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction.normalized * _speed, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponent<Enemy>();
        if (target != null)
        {
            target.TakeDamage(_damage);
            //TODO: Spawn effect
            Destroy(gameObject);
        }
    }
}
