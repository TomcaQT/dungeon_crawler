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

}
