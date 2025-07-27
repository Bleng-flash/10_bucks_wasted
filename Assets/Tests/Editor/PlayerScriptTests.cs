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
        player.Initialise(100f, 100f, 10f);     // manually initialize base class values
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerGO);
        Object.DestroyImmediate(gmGO);
    }

    [Test]
    public void AddXpTest()
    {
        player.AddXp(50f);
        Assert.AreEqual(50f, player.CurrentXp);
    }

    [Test]
    public void RestoreAllHealthTest()
    {
        player.Initialise(100f, 50f, 10f);
        player.RestoreAllHealth();
        Assert.AreEqual(100f, player.CurrentHp);
    }

    [Test]
    public void SetMaxHealthToTest()
    {
        player.SetMaxHealthTo(200f);
        Assert.AreEqual(200f, player.MaxHp);
    }

    [Test]
    public void GetHealthPercentageTest()
    {
        player.Initialise(100f, 50f, 10f);
        player.GetHealthPercentage();
        Assert.AreEqual(0.5f, player.CurrentHp / player.MaxHp);
    }

    [Test]
    public void GetLostHealthTest()
    {
        player.Initialise(100f, 50f, 10f);
        Assert.AreEqual(50f, player.MaxHp - player.CurrentHp);
    }

    [Test]
    public void IncreaseATKByTest()
    {
        player.IncreaseATKBy(10f);
        Assert.AreEqual(20f, player.CurrentATK);
    }

    [Test]
    public void DecreaseATKByTest()
    {
        player.DecreaseATKBy(5f);
        Assert.AreEqual(5f, player.CurrentATK);
    }

    [Test]
    public void GetATKTest()
    {
        player.GetATK();
        Assert.AreEqual(10f, player.CurrentATK);
    }
}
