using UnityEngine;

[CreateAssetMenu(fileName = "MovementStats", menuName = "Movement Stat")]
public class MovementStats : ScriptableObject
{
    public float moveForce;
    public float maxSpeed;
}
