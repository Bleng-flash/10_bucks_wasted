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
        Debug.Log("OnSceneLoaded triggered for scene: " + scene.name);
        // Assign references for scripts that do not persist across scenes 

        // references for GameManager:
        PlayerUIManager playerUIManager = FindFirstObjectByType<PlayerUIManager>();
        XpSpawner xpSpawner = FindFirstObjectByType<XpSpawner>();
        StageManager stageManager = FindFirstObjectByType<StageManager>();
        MessageDisplay messageDisplay = FindFirstObjectByType<MessageDisplay>();

        if (playerUIManager != null) GameManager.Instance.playerUI = playerUIManager;
        if (xpSpawner != null) GameManager.Instance.xpSpawner = xpSpawner;
        if (stageManager != null) GameManager.Instance.stageManager = stageManager;
        if (messageDisplay != null) GameManager.Instance.messageDisplayer = messageDisplay;

        // references for LevelManager:
        TextMeshProUGUI levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>(); // always use this name
        if (levelText != null) LevelManager.Instance.levelText = levelText;

        // references for ScoreManager: nothing

        // references for UpgradeManager:   (use same names)
        GameObject upgradeUI = GameObject.Find("UpgradePanel");
        Transform cardContainer = GameObject.Find("CardContainer").transform;
        GameObject tmp = GameObject.Find("ConfirmSelection button");
        Button confirmButton = null;
        if (tmp != null)
        {
            confirmButton = tmp.GetComponent<Button>();
        }

        if (upgradeUI != null) GameManager.Instance.upgradeManager.upgradeUI = upgradeUI;
        if (cardContainer != null) GameManager.Instance.upgradeManager.cardContainer = cardContainer;
        if (confirmButton != null) GameManager.Instance.upgradeManager.confirmButton = confirmButton;


        PlayerScript player = FindFirstObjectByType<PlayerScript>();

        // references for PlayerUIManager (PlayerUIManager does not persist across scenes)
        if (GameManager.Instance.playerUI != null)
        {
            if (player != null) GameManager.Instance.playerUI.player = player;
            Image healthBar = GameObject.Find("HealthBarFill").GetComponent<Image>();
            if (healthBar != null) GameManager.Instance.playerUI.healthBar = healthBar;
        }


    }

}
