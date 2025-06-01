using UnityEngine;
// Singleton class used to keep track and coordinate game events
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OnPlayerDeath()
    {
        // Bring up Game Over UI
        // Stop all attacks
    }
}
