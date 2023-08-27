using UnityEngine;

[CreateAssetMenu(fileName = "MovementStats", menuName = "Movement Stat")]
public class MovementStats : ScriptableObject
{
    public float moveForce;
    public float maxSpeed;
    public float jumpImpulse;
    [Range(0.1f, 1.0f)] public float decelerationRate;
}
