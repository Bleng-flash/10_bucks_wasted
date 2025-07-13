using TMPro;
using UnityEngine;
// Singleton that keeps track of score
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    // public TextMeshProUGUI scoreText;
    private float currentScore = 0f;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persists across scenes   
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(float score)
    {
        currentScore += score;
    }
    public void ResetScore()
    {
        currentScore = 0;
    }

}
