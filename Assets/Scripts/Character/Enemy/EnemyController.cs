using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Enemy))]
public class EnemyController : MonoBehaviour
{

    protected Transform _target;
    protected NavMeshAgent _agent;

    protected float _damage;
    protected float _attackSpeed;
    protected float timeToAttack;


    private float timeToCollisionAttack;

    protected bool _isLoaded = false;
    
    private void Awake()
    {
        _target = GameObject.Find("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        

        timeToAttack = Random.Range(0f, _attackSpeed);
    }

    public virtual void LoadData(EnemyData enemyData, int roomNumber = 1)
    {
        Debug.Log("Controller loading");
        Awake();
        Start();
        _damage = enemyData.Damage* (1  + (roomNumber * 0.05f));
        _attackSpeed = enemyData.AttackSpeed;
        _isLoaded = true;
    }

    
    private void Update()
    {
        if (!_isLoaded)
            return;
        EnemyBehaviour();
        timeToCollisionAttack += Time.deltaTime;
    }

    protected virtual void EnemyBehaviour()
    {
        _agent.SetDestination(_target.position);
        var distance = Vector3.Distance(transform.position, _target.position);

        //RotateToTarget();

        timeToAttack += Time.deltaTime;
        if (timeToAttack >= _attackSpeed)
        {
            Attack();
            timeToAttack = 0f;
        }
    }

    public virtual void OnHpChanged(object sender, ResourceChangeEventArgs e)
    {
        Debug.Log("HP change");
    }
    
    protected virtual void Attack()
    {
        
    }
    
    private void RotateToTarget()
    {
        Vector3 dir = _target.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && timeToCollisionAttack > 0.5f)
        {
            other.GetComponent<IDamagable>().TakeDamage(_damage);
            timeToCollisionAttack = 0f;
        }
    }
}
