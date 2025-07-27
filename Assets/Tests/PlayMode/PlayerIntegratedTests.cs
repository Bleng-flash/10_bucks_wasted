using TMPro;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class PlayerIntegrationTests
{
    private GameObject playerGO;
    private GameObject gmGO;
    private PlayerScript player;
    private GameObject uiGO;
    private PlayerUIManager playerUI;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Create and init GameManager
        gmGO = new GameObject("GameManager");
        GameManager gm = gmGO.AddComponent<GameManager>();
        GameManager.Instance = gm; // Force set if you use a singleton

        // Create and init PlayerUIManager
        uiGO = new GameObject("PlayerUIManager");
        playerUI = uiGO.AddComponent<PlayerUIManager>();

        // UpgradeManager
        var upgradeManagerGO = new GameObject("UpgradeManager");
        var upgradeManager = upgradeManagerGO.AddComponent<UpgradeManager>();
        UpgradeManager.Instance = upgradeManager;

        // LevelManager (if used)
        var levelManagerGO = new GameObject("LevelManager");
        var levelManager = levelManagerGO.AddComponent<LevelManager>();
        LevelManager.Instance = levelManager;

        GameObject healthGO = new GameObject("HealthBar");
        healthGO.transform.SetParent(uiGO.transform);
        playerUI.healthBar = healthGO.AddComponent<Image>();

        GameObject xpGO = new GameObject("XPBar");
        xpGO.transform.SetParent(uiGO.transform);
        playerUI.SetXpBar(xpGO.AddComponent<Image>());

        // Add dummy level text
        GameObject levelTextGO = new GameObject("LevelText");
        levelTextGO.transform.SetParent(uiGO.transform);
        levelManager.levelText = levelTextGO.AddComponent<TextMeshProUGUI>();

        gm.playerUI = playerUI;

        // Create UpgradeManager and assign it to GameManager
        upgradeManagerGO = new GameObject("UpgradeManager");
        upgradeManager = upgradeManagerGO.AddComponent<UpgradeManager>();
        UpgradeManager.Instance = upgradeManager;
        GameManager.Instance.upgradeManager = upgradeManager; // assign to GameManager field

        // Create player and manually initialize
        playerGO = new GameObject("Player");
        playerGO.AddComponent<Animator>();
        player = playerGO.AddComponent<PlayerScript>();
        player.Initialise(100f, 100f, 10f);

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(playerGO);
        Object.Destroy(gmGO);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerGainsXpAndLevelsUpTest()
    {
        player = playerGO.GetComponent<PlayerScript>();

        // Add XP over time
        player.AddXp(120f);  // more than 100 should trigger level up
        yield return null;

        LogAssert.Expect(LogType.Error, "Upgrade Manager not assigned in GameManager.");
        // We can't check private fields directly, so test side effects
        Assert.AreEqual(2, player.Level);
    }
}
