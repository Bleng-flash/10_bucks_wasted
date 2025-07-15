using UnityEngine;

public class GameOverUI : MonoBehaviour
{
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
