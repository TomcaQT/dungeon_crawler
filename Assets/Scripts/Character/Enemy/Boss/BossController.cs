using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    
    private int _phase = 0;
    private List<float> _phasePercentages;
    private List<float> _phaseDamageMultipliers;
    private List<float> _phaseAttackSpeedBoost;
    
    private GameObject _bullet;
    private MovementType _movementType;
    private List<Vector3> _waypoints;
    private int _currentWaypoint = 0;
    
    
    [SerializeField] private List<SpriteRenderer> _spriteRenderers;
    
    public override void LoadData(EnemyData enemyData)
    {
        base.LoadData(enemyData);
        var bossData = (BossData) enemyData;
        _phasePercentages = bossData.PhasePercentages;
        _phaseDamageMultipliers = bossData.PhaseDamageMultipliers;
        _phaseAttackSpeedBoost = bossData.PhaseAttackSpeedBoost;

        _bullet = bossData.Bullet;
        _movementType = bossData.Movement;
        _waypoints = new List<Vector3>();
        bossData.WaypointsOffset.ForEach(w => _waypoints.Add(transform.position + new Vector3(w.x,w.y,0)));
        //_agent.stoppingDistance = shootingEnemyData.StopDistance;
    }
    
    protected override void Attack()
    {
        var bulletObj = Instantiate(_bullet, transform.position, Quaternion.identity);
        var bullet = bulletObj.GetComponent<Bullet>();
        bullet.Initialize(gameObject,_damage);
        
        
        Vector2 lookingDir =  _target.position - transform.position;
        bullet.Shoot(lookingDir);
        
    }
    
    protected override void EnemyBehaviour()
    {
        if(_movementType == MovementType.Chase)
            _agent.SetDestination(_target.position);
        else if (_movementType == MovementType.Patrol)
        {
            _agent.SetDestination(_waypoints[_currentWaypoint]);
            var distance = Vector3.Distance(transform.position, _waypoints[_currentWaypoint]);
            if (distance <= 0.1f)
                Utils.GetNextOver<Vector3>(_waypoints, ref _currentWaypoint);
        }
        

        //RotateToTarget();

        timeToAttack += Time.deltaTime;
        if (timeToAttack >= _attackSpeed)
        {
            Attack();
            timeToAttack = 0f;
        }
    }
    
    
    
    public override void OnHpChanged(object sender, ResourceChangeEventArgs e)
    {
        base.OnHpChanged(sender,e);

        if (_phase >= _phasePercentages.Count)
            return;
        
        if(CurrentPhasePercentages > e.Percentage01)
            NextPhase();
    }

    public void NextPhase()
    {
        if (_phase >= _phaseDamageMultipliers.Count)
            return;
        _damage *= _phaseDamageMultipliers[_phase];
        _attackSpeed -= _phaseAttackSpeedBoost[_phase];
        _spriteRenderers.ForEach(x => x.color = Color.red);
        _phase++;
    }

    public int Phase => _phase;
    public float CurrentPhasePercentages => _phasePercentages[_phase];

}
