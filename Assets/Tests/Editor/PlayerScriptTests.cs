using NUnit.Framework;
using UnityEngine;

public class PlayerScriptTests
{
    private GameObject playerGO;
    private GameObject gmGO;
    private PlayerScript player;

    [SetUp]
    public void SetUp()
    {
        gmGO = new GameObject("GameManager");
        gmGO.AddComponent<MockGameManager>();

        playerGO = new GameObject();
        playerGO.AddComponent<Animator>(); 
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

    [Test]
    public void TakeDamage_DecreasesHpCorrectly()
    {
        player.Initialise(100f, 100f, 10f);
        player.TakeDamage(50f);
        Assert.AreEqual(50f, player.CurrentHp);
    }
}
