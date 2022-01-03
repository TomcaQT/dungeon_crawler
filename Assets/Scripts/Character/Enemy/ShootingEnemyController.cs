using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class ShootingEnemyController : EnemyController
{

    private GameObject _bullet;

   // [SerializeField]private ShootingEnemyData _enemyData;
    
    public override void LoadData()
    {
        base.LoadData();
        //_bullet = _enemyData.BulletProjectile;

    }
    
    protected override void Attack()
    {
        var bulletObj = Instantiate(_bullet, transform.position, Quaternion.identity);
        var bullet = bulletObj.GetComponent<Bullet>();
        bullet.Initialize(_damage);
        
        
        Vector2 lookingDir =  _target.position - transform.position;
        bullet.Shoot(lookingDir);
        
    }
}
