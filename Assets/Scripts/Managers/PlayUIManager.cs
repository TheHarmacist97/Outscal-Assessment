using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayUIManager : MonoBehaviour
{
    public static PlayUIManager Instance;

    [Header("Post Game UI")]
    [SerializeField] private Image holder;
    [SerializeField] private Color winColor, loseColor;
    [SerializeField] private TextMeshProUGUI outcomeTextField;

    [Header("Player Health UI")]
    [SerializeField] private Image fillImage;

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
    }

    #region Post-Game Functions
    public void ShowOptions(bool win)
    {
        holder.gameObject.SetActive(true);
        outcomeTextField.text = win ? "You won! Wanna retry?" : "Lost this one chief, try again";
        holder.color = win ? winColor : loseColor;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    #endregion

    #region Health Functions

    public void ShowHealth(float healthPercentage)
    {
        fillImage.fillAmount = healthPercentage;
    }

    #endregion

}