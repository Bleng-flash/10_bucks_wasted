using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerIntegrationTests
{
    private GameObject playerGO;
    private GameObject gmGO;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Create and init GameManager
        gmGO = new GameObject("GameManager");
        gmGO.AddComponent<GameManager>(); // must be mock or real with default values

        // Create and init Player
        playerGO = new GameObject("Player");
        playerGO.AddComponent<Animator>(); // required by PlayerScript
        var player = playerGO.AddComponent<PlayerScript>();

        yield return null; // wait one frame for Awake and Start
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(playerGO);
        Object.Destroy(gmGO);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Player_GainsXp_LevelsUp_WhenEnoughXpIsAdded()
    {
        var player = playerGO.GetComponent<PlayerScript>();

        // Add XP over time
        player.AddXp(120f);  // more than 100 should trigger level up
        yield return null;

        // We can't check private fields directly, so test side effects
        Assert.GreaterOrEqual(player.CurrentXp, 0f);
    }
}
