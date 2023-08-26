using UnityEngine;

[CreateAssetMenu(fileName = "HealthAsset", menuName = "Health Stat")]
public class HealthStats : ScriptableObject
{
    public float maxHealth;
    public float regenDelay;
    public float regenRate;
}
