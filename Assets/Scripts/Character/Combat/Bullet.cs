using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxLifetime = 5f;

    [SerializeField] private float _damage = 10f;

    private GameObject _sender;
    
    private void Start()
    {
        Destroy(gameObject,_maxLifetime);
    }

    public void SetSender(GameObject sender) => _sender = sender;
    
    public void Initialize(GameObject sender,float damage = 10f, float speed = 10f, float maxLifetime = 5f)
    {
        _sender = sender;
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
        var target = other.GetComponent<IDamagable>();
        if (target != null && other.gameObject != _sender)
        {
            target.TakeDamage(_damage);
            //TODO: Spawn effect
            Destroy(gameObject);
        }
    }
}
