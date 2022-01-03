using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxLifetime = 5f;

    [SerializeField] private float _damage = 10f;
    [SerializeField] private bool _isReflecting = false; 
    private GameObject _sender;

    private Vector3 _direction;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        //_collider.enabled = false;
    }

    private void Start()
    {
        Destroy(gameObject,_maxLifetime);
    }

    public void SetSender(GameObject sender) => _sender = sender;
    
    public void Initialize(GameObject sender,float damage = 10f, float speed = 10f, float maxLifetime = 5f)
    {
        _sender = sender;
        gameObject.layer = sender.layer;
        _damage = damage;
        _speed = speed;
        _maxLifetime = maxLifetime;
    }
    
    public void Shoot(Vector3 direction)
    {
        //_collider.enabled = true;
        //_rigidbody.AddForce(direction.normalized * _speed, ForceMode2d);
        _rigidbody.velocity = direction.normalized * _speed;
        _direction = direction.normalized;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == gameObject.layer)
            return;
        
        var target = other.GetComponent<IDamagable>();
        if (target != null)
        {
            target.TakeDamage(_damage);
            //TODO: Spawn effect
            Destroy(gameObject);
        }
        else if (target == null && !other.TryGetComponent<Bullet>(out var bullet)) // Not damagable
        {
            if (!_isReflecting)
            {
                Destroy(gameObject);
                return;
            }
            ContactPoint2D[] contacts = new ContactPoint2D[10];
            other.GetContacts(contacts);

            Vector3 currentBulletMoveVector = _direction;
            Vector3 newBulletMoveVector = Vector3.Reflect(currentBulletMoveVector, contacts[0].normal);
            Shoot(newBulletMoveVector);
        }
        
    }
}
