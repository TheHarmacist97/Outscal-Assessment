using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthStats playerHealthStats;
    public float currentHealth;
    public bool isAlive = true;

    public delegate void OnDamage();
    public static event OnDamage OnDamageEvent;

    public delegate void OnDeath();
    public static event OnDeath OnDeathEvent;

    private Coroutine regenCoroutine;
    private WaitForSeconds regenDelayWait;
    private bool regenerating = true;

    private void Awake()
    {
        isAlive = true;
        currentHealth = playerHealthStats.maxHealth;
        regenDelayWait = new WaitForSeconds(playerHealthStats.regenDelay);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(3f);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        PlayUIManager.Instance.ShowHealth(currentHealth / playerHealthStats.maxHealth);
        if (currentHealth <= 0)
        {
            Kill();
        }
        else
        {
            if (regenCoroutine != null)
                StopCoroutine(regenCoroutine);
            regenCoroutine = StartCoroutine(StartRegen());
        }

        OnDamageEvent?.Invoke();
    }

    public void Kill()
    {
        regenerating = false;
        isAlive = false;
        StopAllCoroutines();
        OnDeathEvent?.Invoke();
    }

    private IEnumerator StartRegen()
    {
        regenerating = false;
        yield return regenDelayWait;
        regenerating = true;

        while (currentHealth <= playerHealthStats.maxHealth)
        {
            yield return null;
            currentHealth += Time.deltaTime * playerHealthStats.regenRate;
            PlayUIManager.Instance.ShowHealth(currentHealth / playerHealthStats.maxHealth);
            if (!regenerating)
            {
                yield break;
            }
        }
        currentHealth = Mathf.Clamp(currentHealth, 0, playerHealthStats.maxHealth);
    }

}
