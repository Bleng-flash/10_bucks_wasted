
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

// Upon every level up of the player, the game freezes and 3 upgrade cards appear on the screen (UI)
// the player will pick 1 of the 3 upgrades
// Don't make UpgradeManager a singleton bcos it is referenced as a field in GameManager

public class UpgradeManager : MonoBehaviour
{
    // This callback will be called when upgrade is finished
    [SerializeField] private GameObject upgradeUI; // Reference to the upgrade UI panel
    [SerializeField] private List<Upgrade> allUpgrades; // list of upgrades available
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardContainer;
    [SerializeField] private Button confirmButton;
    private Action onUpgradeComplete; // Action is a type that represents a method that returns void
    private Upgrade selectedUpgrade;
    private List<GameObject> activeCards = new(); // the 3 upgrade cards that player can pick from

    private void Awake()
    {
        if (upgradeUI != null)
        {
            upgradeUI.SetActive(false);  // upgradeUI is hidden initially
        }
        confirmButton.interactable = false; // we make it interactable after the player has chosen a card
    }

    // Called by GameManager to open the upgrade screen
    public void ShowUpgradeScreen(Action onComplete)
    {
        onUpgradeComplete = onComplete;
        if (upgradeUI == null)
        {
            return;
        }
        GameManager.Instance.PauseGame();
        upgradeUI.SetActive(true);
        selectedUpgrade = null;
        DisplayUpgradeOptions();
    }

    private void DisplayUpgradeOptions()
    {
        ClearOldCards();
        List<Upgrade> options = GetRandomUpgrades(3);
        foreach (Upgrade upgrade in options)
        {
            GameObject card = Instantiate(cardPrefab, cardContainer); // instantiates a card gameobject 
            // based on the prefab as a child of cardContainer
            SetupCardUI(card, upgrade);
            activeCards.Add(card);
        }

    }

    private List<Upgrade> GetRandomUpgrades(int count)
    {
        List<Upgrade> pool = new(allUpgrades);
        List<Upgrade> selected = new();

        while (selected.Count < count && pool.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, pool.Count);
            selected.Add(pool[index]);
            pool.RemoveAt(index);
        }

        return selected;
    }


    private void SetupCardUI(GameObject card, Upgrade upgrade)
    {
        Debug.Log($"Set up card for upgrade: {upgrade.upgradeName}");
        card.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = upgrade.upgradeName;
        card.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = upgrade.description;
        card.transform.Find("Icon").GetComponent<Image>().sprite = upgrade.icon;

        Button button = card.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            selectedUpgrade = upgrade;
            HighlightCard(card);
            confirmButton.interactable = true;
        });
    }


    private void HighlightCard(GameObject selectedCard)
    {
        foreach (GameObject card in activeCards)
        {
            card.GetComponent<Image>().color = (card == selectedCard) ? Color.yellow : Color.white;
        }
    }

    private void ClearOldCards()
    {
        foreach (GameObject card in activeCards)
        {
            Destroy(card);
        }
        activeCards.Clear();
    }

    // this ends the upgradeUI and resumes game
    public void ConfirmSelection(PlayerScript player)
    {
        if (selectedUpgrade != null)
        {
            selectedUpgrade.ApplyUpgrade(player);
            ClearOldCards();
            upgradeUI.SetActive(false); // exit UI screen
            GameManager.Instance.ResumeGame();
            onUpgradeComplete?.Invoke();
        }
    }
    public void ConfirmSelectionButtonHandler()
    {
        PlayerScript player = GameObject.FindWithTag("Player")?.GetComponent<PlayerScript>();
        if (player != null)
        {
            ConfirmSelection(player);
        }
    }

}