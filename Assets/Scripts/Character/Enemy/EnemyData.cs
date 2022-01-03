using System.Collections;
using System.Collections.Generic;
using Items;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseEnemyData",menuName = "Enemies/BaseEnemy")]
public class EnemyData : ScriptableObject
{

    public string Name;

    public string Description; //???

    public float Damage;

    public LootTable LootTable;
    
    [Header("Movement")] public float StopDistance = 0f;

}


[CreateAssetMenu(fileName = "ShootingEnemyData",menuName = "Enemies/ShootingEnemy")]
public class ShootingEnemyData : EnemyData
{

    //Just setting default values to inherited properties
    public ShootingEnemyData()
    {
        StopDistance = 5f;
    }

    [Header("Shooting")] public GameObject BulletProjectile;
}
