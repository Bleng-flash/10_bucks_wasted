using UnityEngine;
// Singleton class used to keep track and coordinate game events

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isPlayerAlive = true;
    [SerializeField] private SwitchScene sceneSwitcher;
    [SerializeField] private PlayerUIManager playerUI;

    [Header("References")]
    public XpSpawner xpSpawner;

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
        sceneSwitcher.LoadGameOverScene();
        // Debug.Log("Game Over!");
    }

    // Tells playerUI to update health when player receives damage
    public void OnPlayerDamage(float current, float max)
    {
        playerUI.UpdateHealth(current, max);
    }

    public void UpdateXp(float current, float max)
    {
        playerUI.UpdateXp(current, max);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // pauses any timers that depend on Time.deltaTime
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

}
