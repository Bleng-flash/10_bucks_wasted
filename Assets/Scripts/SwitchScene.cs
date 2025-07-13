using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// singleton class
public class SwitchScene : MonoBehaviour
{
    private static SwitchScene instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene"); // match scene name exactly
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }
    public void LoadChapter1Wave()
    {
        SceneManager.LoadScene("Chapter1-Wave");
    }
    public void LoadChapter1Boss()
    {
        SceneManager.LoadScene("Chapter1-Boss");
    }
    public void LoadChapter2Wave()
    {
        SceneManager.LoadScene("Chapter2-Wave");
    }
    public void LoadChapter2Boss()
    {
        SceneManager.LoadScene("Chapter2-Boss");
    }
    public void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes in build settings");
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");

        // --- References for GameManager ---
        PlayerUIManager playerUIManager = FindFirstObjectByType<PlayerUIManager>();
        XpSpawner xpSpawner = FindFirstObjectByType<XpSpawner>();
        StageManager stageManager = FindFirstObjectByType<StageManager>();
        MessageDisplay messageDisplay = FindFirstObjectByType<MessageDisplay>();

        if (playerUIManager != null)
        {
            GameManager.Instance.playerUI = playerUIManager;
            // Debug.Log("Assigned PlayerUIManager");
        }

        if (xpSpawner != null)
        {
            GameManager.Instance.xpSpawner = xpSpawner;
            // Debug.Log("Assigned XpSpawner");
        }

        if (stageManager != null)
        {
            GameManager.Instance.stageManager = stageManager;
            // Debug.Log("Assigned StageManager");
        }

        if (messageDisplay != null)
        {
            GameManager.Instance.messageDisplayer = messageDisplay;
            // Debug.Log("Assigned MessageDisplay");
        }

        // --- References for LevelManager ---
        GameObject levelGO = GameObject.Find("LevelText");
        if (levelGO != null)
        {
            TextMeshProUGUI levelText = levelGO.GetComponent<TextMeshProUGUI>();
            if (levelText != null)
            {
                LevelManager.Instance.levelText = levelText;
                Debug.Log("Assigned Level Text");
            }
        }

        // --- References for UpgradeManager ---
        GameObject upgradeUI = GameObject.Find("UpgradePanel");
        GameObject cardContainerGO = GameObject.Find("CardContainer");
        GameObject confirmButtonGO = GameObject.Find("ConfirmSelection button");

        if (cardContainerGO != null)
        {
            GameManager.Instance.upgradeManager.cardContainer = cardContainerGO.transform;
            Debug.Log("Assigned Card Container");
        }

        if (confirmButtonGO != null)
        {
            Button confirmButton = confirmButtonGO.GetComponent<Button>();
            if (confirmButton != null)
            {
                GameManager.Instance.upgradeManager.confirmButton = confirmButton;
                Debug.Log("Assigned Confirm Button");
            }
        }

        if (upgradeUI != null)
        {
            GameManager.Instance.upgradeManager.upgradeUI = upgradeUI;
            Debug.Log("Assigned Upgrade UI");
            GameManager.Instance.upgradeManager.upgradeUI.SetActive(false);  // upgradeUI is hidden initially
            GameManager.Instance.upgradeManager.confirmButton.interactable = false;
        }



        // --- Player reference ---
        PlayerScript player = FindFirstObjectByType<PlayerScript>();
        if (player == null)
        {
            Debug.LogWarning("PlayerScript not found in scene.");
            return;
        }

        // --- PlayerUIManager assignments ---
        if (GameManager.Instance.playerUI != null)
        {
            GameManager.Instance.playerUI.player = player;

            GameObject healthBarGO = GameObject.Find("HealthBarFill");
            if (healthBarGO != null)
            {
                Image healthBar = healthBarGO.GetComponent<Image>();
                if (healthBar != null)
                {
                    GameManager.Instance.playerUI.healthBar = healthBar;
                    Debug.Log("Assigned HealthBar");
                }
            }
        }

        // --- Enemy Spawners ---
        EnemySpawning[] spawners = FindObjectsOfType<EnemySpawning>();
        foreach (EnemySpawning spawner in spawners)
        {
            spawner.player = player.transform;
            Debug.Log($"Assigned player to spawner: {spawner.name}");
        }

        // set camera follow player
        
    }

}
