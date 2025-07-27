using UnityEngine;

public class MockGameManager : MonoBehaviour
{
    public static MockGameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateXp(float current, float required) { }
    public void OnPlayerDamage(float currentHp, float maxHp) { }
    public void OnPlayerDeath() { }
    public void ShowUpgradeScreen(System.Action onUpgradeComplete)
    {
        // Simulate upgrade completion immediately
        onUpgradeComplete?.Invoke();
    }

    public void DisplayMessage(string message, float duration, int fontSize) { }
    public void TeleportPlayer() { }
}
