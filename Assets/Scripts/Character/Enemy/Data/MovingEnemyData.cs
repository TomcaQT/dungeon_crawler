using UnityEngine;

[CreateAssetMenu(fileName = "MovingEnemyData",menuName = "Enemies/MovingEnemy")]
public class MovingEnemyData : EnemyData
{
    public bool FollowPlayer;
    public Vector3 Direction;
}