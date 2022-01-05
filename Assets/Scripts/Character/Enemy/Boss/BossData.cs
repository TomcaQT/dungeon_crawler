using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossData",menuName = "Boss/BossData")]
public class BossData : EnemyData
{
    public List<float> PhasePercentages;

    public List<float> PhaseDamageMultipliers;

    public List<float> PhaseAttackSpeedBoost;


}
