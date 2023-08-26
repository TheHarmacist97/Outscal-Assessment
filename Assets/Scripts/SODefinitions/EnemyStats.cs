using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy Stat")]
public class EnemyStats : ScriptableObject
{
    public float damage;
    public float patrolDuration;
}
