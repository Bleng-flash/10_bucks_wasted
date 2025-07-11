using TMPro;
using UnityEngine;
// Singleton that keeps track of score
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    private float currentScore = 0;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        // Initialise score as 0
        UpdateScore();
    }

    public void AddScore(float score)
    {
        currentScore += score;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + currentScore;
    }
}
