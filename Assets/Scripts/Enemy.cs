using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private Vector2 positionA, positionB;
    private Collider coll;
    void Start()
    {
        //preserving position in case path points are children of this enemy
        positionA = pointA.position;
        positionB = pointB.position;
        StartCoroutine(PatrolArea());
    }

    private IEnumerator PatrolArea()
    {
        float elapsedTime = 0f;
        while (true)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            float tVal = Mathf.Sin(elapsedTime / stats.patrolDuration) * 0.5f + 0.5f; //remapping sin value from (-1 to 1) to (0,1)
            tVal = tVal * tVal * (3 - 2 * tVal);  //some basic smoothing
            transform.position = Vector2.Lerp(positionA, positionB, tVal);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable healthComponent))
        {
            healthComponent.TakeDamage(stats.damage);
        }
    }
}
