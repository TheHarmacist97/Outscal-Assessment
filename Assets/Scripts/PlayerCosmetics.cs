using System.Collections;
using UnityEngine;

public class PlayerCosmetics : MonoBehaviour
{
    [SerializeField] private float flashTime;
    [SerializeField] private Color flashColor;
    private SpriteRenderer spriteRenderer;
    private WaitForSeconds flashTimeWait;
    private Color originalColor;

    private Coroutine flashCoroutine;

    private void OnEnable()
    {
        Player.OnDamageEvent += Player_OnDamageEvent;
        Player.OnDeathEvent += Player_OnDeathEvent;
    }

    private void OnDisable()
    {
        Player.OnDamageEvent -= Player_OnDamageEvent;
        Player.OnDeathEvent -= Player_OnDeathEvent;
    }

    private void Awake()
    {
        flashTimeWait = new WaitForSeconds(flashTime);

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Player_OnDamageEvent()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            spriteRenderer.color = originalColor;
        }
        flashCoroutine = StartCoroutine(StartFlash());
    }
    private void Player_OnDeathEvent()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            spriteRenderer.color = originalColor;
        }

        StartCoroutine(StartDecay());
    }

    private IEnumerator StartDecay()
    {
        float alpha = 1f;
        while (alpha >= 0)
        {
            yield return null;
            alpha -= Time.deltaTime * 0.5f;
            Color tempColor = spriteRenderer.color;
            tempColor.a = alpha;
            spriteRenderer.color = tempColor;
        }
    }

    private IEnumerator StartFlash()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = flashColor;
            yield return flashTimeWait;
            spriteRenderer.color = originalColor;
            yield return flashTimeWait;
        }
    }
}
