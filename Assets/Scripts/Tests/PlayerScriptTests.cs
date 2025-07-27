using NUnit.Framework;
using UnityEngine;

public class PlayerScriptTests
{
    private GameObject playerGO;
    private PlayerScript player;

    [SetUp]
    public void SetUp()
    {
        playerGO = new GameObject();
        player = playerGO.AddComponent<PlayerScript>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerGO);
    }

    [Test]
    public void AddXp_IncreasesXpCorrectly()
    {
        player.AddXp(50f);
        Assert.AreEqual(50f, player.CurrentXp);
    }
}
