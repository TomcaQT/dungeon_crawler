using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    
    private int _phase = 0;
    private List<float> _phasePercentages;
    private List<float> _phaseDamageMultipliers;
    
    
    public override void LoadData(EnemyData enemyData)
    {
        base.LoadData(enemyData);
        var bossData = (BossData) enemyData;
        _phasePercentages = bossData.PhasePercentages;
        _phaseDamageMultipliers = bossData.PhaseDamageMultipliers;

        //_bullet = shootingEnemyData.BulletProjectile;
        //_agent.stoppingDistance = shootingEnemyData.StopDistance;
    }
    
    protected override void Attack()
    {
        //var bulletObj = Instantiate(_bullet, transform.position, Quaternion.identity);
        //var bullet = bulletObj.GetComponent<Bullet>();
        //bullet.Initialize(gameObject,_damage);
        
        
        //Vector2 lookingDir =  _target.position - transform.position;
        //bullet.Shoot(lookingDir);
        
    }
    
    protected override void EnemyBehaviour()
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

    public void NextPhase()
    {
        if (_phase >= _phaseDamageMultipliers.Count)
            return;
        _damage *= _phaseDamageMultipliers[_phase];
        _phase++;
    }
    
    public float CurrentPhasePercentages => _phasePercentages[_phase];

}
