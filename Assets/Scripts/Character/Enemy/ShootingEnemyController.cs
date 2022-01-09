using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class ShootingEnemyController : EnemyController
{

    private GameObject _bullet;

    
    public override void LoadData(EnemyData enemyData, int roomNumber)
    {
        base.LoadData(enemyData);
        _isLoaded = false;
        var shootingEnemyData = (ShootingEnemyData) enemyData;
        _bullet = shootingEnemyData.BulletProjectile;

        _agent.stoppingDistance = shootingEnemyData.StopDistance;
        _isLoaded = true;
    }
    
    protected override void Attack()
    {
        var bulletObj = Instantiate(_bullet, transform.position, Quaternion.identity);
        var bullet = bulletObj.GetComponent<Bullet>();
        bullet.Initialize(gameObject,_damage);
        
        
        Vector2 lookingDir =  _target.position - transform.position;
        bullet.Shoot(lookingDir);
        
    }
}
