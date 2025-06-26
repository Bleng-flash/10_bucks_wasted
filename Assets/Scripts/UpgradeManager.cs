
using UnityEngine;
using System;

// Upon every level up of the player, the game freezes and 3 upgrade cards appear on the screen (UI)
// the player will pick 1 of the 3 upgrades

public class UpgradeManager : MonoBehaviour
{
    // This callback will be called when upgrade is finished
    private Action onUpgradeComplete; // Action is a type that represents a method that returns void
    [SerializeField] private GameObject upgradeUI; // Reference to the upgrade UI panel

    private void Awake()
    {
        if (upgradeUI != null)
        {
            upgradeUI.SetActive(false);  // upgradeUI is hidden initially
        }
    }

    // Called by GameManager to open the upgrade screen
    public void ShowUpgradeScreen(Action onComplete)
    {
        onUpgradeComplete = onComplete;
        if (upgradeUI != null)
        {
            GameManager.Instance.PauseGame();
            upgradeUI.SetActive(true);
        }
    }

    // Called when the player confirms the upgrade and closes the screen
    public void CompleteUpgrade()
    {
        if (upgradeUI != null)
        {
            upgradeUI.SetActive(false);
        }
        onUpgradeComplete?.Invoke();
        GameManager.Instance.ResumeGame();
    }

    // Implement upgrade logic here (e.g., selecting a card, applying upgrades, etc.)
}