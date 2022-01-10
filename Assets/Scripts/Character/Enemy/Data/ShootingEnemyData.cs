using UnityEngine;

[CreateAssetMenu(fileName = "ShootingEnemyData",menuName = "Enemies/ShootingEnemy")]
public class ShootingEnemyData : EnemyData
{

        
    [Header("Shooting")] public GameObject BulletProjectile;
    public float ProjectileSpeed = 10f;
    public float ProjectileLifetime = 5f;

}