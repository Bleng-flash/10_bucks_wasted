using TMPro;
using UnityEngine;
// Singleton that keeps track of player level

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public TextMeshProUGUI levelText;
    private int currentLevel = 1;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    void Start()
    {
        // Initialise level as 1
        UpdateLevel();
    }

    public void LevelUp()
    {
        currentLevel++;
        UpdateLevel();
    }

    public void UpdateLevel()
    {
        if (levelText != null)
            levelText.text = "Level " + currentLevel;
        else
        Debug.LogWarning("Level text UI not assigned.");
    }
    public void ResetLevel()
    {
        currentLevel = 1;
        UpdateLevel();
    }
}
