using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    // display score
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