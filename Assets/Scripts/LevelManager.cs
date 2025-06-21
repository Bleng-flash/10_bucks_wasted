using TMPro;
using UnityEngine;
// Singleton that keeps track of player level

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private TextMeshProUGUI levelText;
    private int currentLevel = 1;
    void Awake()
    {
        if (Instance == null) Instance = this;
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

    private void UpdateLevel()
    {
        levelText.text = "Level: " + currentLevel;
    }
}
