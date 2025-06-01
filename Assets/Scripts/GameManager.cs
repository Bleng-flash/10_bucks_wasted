using UnityEngine;
// Singleton class used to keep track and coordinate game events
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isPlayerAlive = true;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OnPlayerDeath()
    {
        isPlayerAlive = false;
        // Bring up Game Over UI
    }
}
