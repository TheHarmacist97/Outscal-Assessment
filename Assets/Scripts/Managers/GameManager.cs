using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance);
            Instance = this;
        }
        Player.OnDeathEvent += Lose;
    }

    public void Win()
    {
        PlayUIManager.Instance.ShowOptions(true);
    }

    public void Lose()
    {
        PlayUIManager.Instance.ShowOptions(false);
    }


}
