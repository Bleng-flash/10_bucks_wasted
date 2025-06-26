using UnityEngine;
using System;
// Singleton class used to keep track and coordinate game events

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isPlayerAlive = true;
    [SerializeField] private SwitchScene sceneSwitcher;
    [SerializeField] private PlayerUIManager playerUI;

    [Header("References")]
    public XpSpawner xpSpawner;
    [SerializeField] private Upgrade upgradeManager;

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

    // PauseGame() forcibly pauses movement for player and all enemies
    // and pauses any enemy spawning, any attacks, and the timer since they use Time.deltaTime
    public void PauseGame()
    {
        Time.timeScale = 0f; // sets Time.deltaTime to 0 (stops advancing time)
        SetPlayerState(false);
        SetEnemyState(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        SetPlayerState(true);
        SetEnemyState(true);
    }


    // PauseGame() restarts movement and attacks for player and all enemies
    private void SetPlayerState(bool isActive)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerMovement move = player.GetComponent<PlayerMovement>();
            if (move) move.enabled = isActive;
        }
    }

    private void SetEnemyState(bool isActive)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            EnemyMovement move = enemy.GetComponent<EnemyMovement>();
            if (move) move.enabled = isActive;
        }
    }

    public void ShowUpgradeScreen(Action onUpgradeComplete)
    {
        if (upgradeManager != null)
        {
            upgradeManager.ShowUpgradeScreen(onUpgradeComplete);
        }
        else
        {
            Debug.LogError("Upgrade Manager not assigned in GameManager.");
        }
    }

}
