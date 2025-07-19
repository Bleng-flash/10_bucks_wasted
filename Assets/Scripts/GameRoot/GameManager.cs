using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// Singleton class used to keep track and coordinate game events

public class GameManager : MonoBehaviour
{
    public PlayerScript player;
    public static GameManager Instance;
    public bool isPlayerAlive = true;
    public SwitchScene sceneSwitcher;
    public PlayerUIManager playerUI;

    [Header("References")]
    public XpSpawner xpSpawner;
    public UpgradeManager upgradeManager;
    public StageManager stageManager;
    public MessageDisplay messageDisplayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
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
        if (playerUI != null)
            playerUI.UpdateXp(current, max);
        else
            Debug.LogWarning("playerUI not assigned in GameManager.");
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

    public void TeleportPlayer()
    {
        stageManager.ProceedToNextStage(sceneSwitcher);
    }

    public void DisplayMessage(String message, float duration, int fontSize)
    {
        messageDisplayer.ShowMessage(message, duration, fontSize);
    }

    public void ResetRun()
    {
        // Reset persistent flags and time
        isPlayerAlive = true;
        Time.timeScale = 1f;

        // Reset Score
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }

        // Reset Player Level
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.ResetLevel();
        }


        // Destroy player gameobject
        if (player != null) Destroy(player.gameObject);

        // Reset upgradeManager gameobject
        if (upgradeManager != null)
        {
            upgradeManager.ResetAllUpgrades(); // reset all weights and applyCounts of upgrades
        }

        // Destroy all enemies if still in current scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        sceneSwitcher.LoadStartScene();
        
        // Subscribe to sceneLoaded to assign player after scene loads
        SceneManager.sceneLoaded += OnStartSceneLoaded;
        // we need to do this because scene loading is asynchronous 
    }

    // only called on resetting runs
    private void OnStartSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartScene")
        {
            PlayerScript player1 = FindAnyObjectByType<PlayerScript>();
            player = player1;

            // Unsubscribe to avoid multiple calls
            SceneManager.sceneLoaded -= OnStartSceneLoaded;
        }
    }
}