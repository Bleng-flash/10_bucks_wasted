using UnityEngine;
using TMPro;

// exactly the same logic as the GameOverUI script
public class WinSceneUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    void Start()
    {
        if (ScoreManager.Instance != null)
        {
            int score = ScoreManager.Instance.GetScoreToInteger();
            scoreText.text = "Your Score is: " + score.ToString();
        }
    }

    public void OnPlayAgainPressed()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetRun();
        }
        else
        {
            Debug.LogError("GameManager instance not found.");
        }
    }

}
