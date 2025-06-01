using UnityEngine;
// Singleton class used to keep track and coordinate game events

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isPlayerAlive = true;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private PlayerUIManager playerUI;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OnPlayerDeath()
    {
        // Set player death flag for various scripts to carry out player death event actions
        isPlayerAlive = false;

        // Bring up Game Over UI
        gameOverText.SetActive(true);
        Debug.Log("Game Over!");
    }

    // Tells playerUI to update health when player receives damage
    public void OnPlayerDamage()
    {
        playerUI.UpdateHealth();
    }
}
