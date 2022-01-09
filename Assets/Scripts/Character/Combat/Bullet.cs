using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxLifetime = 5f;

    [SerializeField] private float _damage = 10f;
    [SerializeField] private bool _isReflecting = false;
    [SerializeField] private GameObject _extraSpawn;
    [SerializeField] private int _extraSpawnCount;
    [SerializeField] private float _extraAngle;
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

        var dir1 = new Vector3(_direction.x,_direction.y,_direction.z);
        var dir2 = new Vector3(_direction.x,_direction.y,_direction.z);
        for (int i = 0; i < _extraSpawnCount; i++)
        {
            var extraBullet1 = Instantiate(_extraSpawn, transform.position, Quaternion.identity).GetComponent<Bullet>();
            var extraBullet2 = Instantiate(_extraSpawn, transform.position, Quaternion.identity).GetComponent<Bullet>();
            dir1 = Quaternion.AngleAxis(-_extraAngle, Vector3.forward) * dir1;
            dir2 = Quaternion.AngleAxis(_extraAngle, Vector3.forward) * dir2;
            extraBullet1.Initialize(_sender,_damage,_speed,_maxLifetime);
            extraBullet1.Shoot(dir1);
            extraBullet2.Initialize(_sender,_damage,_speed,_maxLifetime);
            extraBullet2.Shoot(dir2);
        }
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
        else if (!other.TryGetComponent<ICollectible>(out var col) && !other.TryGetComponent<Bullet>(out var bullet)) // Not damagable
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
