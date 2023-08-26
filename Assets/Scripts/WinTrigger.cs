using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitLayerMask = 1 << collision.gameObject.layer;
        if (hitLayerMask == playerLayer)
        {
            GameManager.Instance.Win();
        }
    }
}
