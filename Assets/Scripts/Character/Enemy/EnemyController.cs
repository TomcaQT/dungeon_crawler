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

    public virtual void LoadData(EnemyData enemyData)
    {
        _damage = enemyData.Damage;
        _attackSpeed = enemyData.AttackSpeed;
    }

    
    private void Update()
    {
        EnemyBehaviour();
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

    protected virtual void Attack()
    {
        
    }
    
    private void RotateToTarget()
    {
        Vector3 dir = _target.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    
    
    
}
