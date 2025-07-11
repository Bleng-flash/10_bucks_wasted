using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

/* 
    Player inherits from Entity
    In addition to stats and attacks provided by Entity, player has player health bar, 
    player XP bar and plaeyr XP (KIV) as well
*/

public class PlayerScript : Entity
{
    private float xpAmount;
    private float xpToNextLevel;
    private int level;
    private bool isLevelingUp = false;
    // player must only use autoattacks
    private Dictionary<string, AutoAttack> allAttacks = new(); // keyed on attack scripts name
    private List<AutoAttack> activeAttacks = new();
    private bool hasTeleporter = false;

    [SerializeField] private XpScript xpScript;
    [SerializeField] private float xpPickUpRadius = 2.0f;
    [SerializeField] private LayerMask xpLayer;

    private void Awake()
    {
        // Initialising player stats and xp 
        team = Team.Player;
        xpAmount = 0;
        xpToNextLevel = 100;
        level = 1;
        Initialise(maxHP, HP, ATK);
    }

    void Start()
    {
        GameManager.Instance.UpdateXp(xpAmount, xpToNextLevel);     // Initialise xp bar to be empty

        // Find all attack components in children 
        AutoAttack[] attacks = GetComponentsInChildren<AutoAttack>(true); // include inactive
        foreach (AutoAttack attack in attacks)
        {
            attack.gameObject.SetActive(false);  // disable everything first
            attack.tier = 0;
            allAttacks[attack.GetType().Name] = attack;  // use class name as key
        }

        // Enable starting attack
        UnlockOrUpgradeAttack("AOEPunch");
    }

    // Every frame, pick up any xp in range and check if can level up
    void Update()
    {
        if (isLevelingUp) return;
        // Disallows XP pickup and multiple level ups while player is picking upgrade

        PickUpXp();
        if (xpAmount >= xpToNextLevel)
        {
            LevelUp();
            Debug.Log("Next xp required to level up: " + xpToNextLevel);
            Debug.Log("Current level: " + level);
        }

        if (hasTeleporter && Keyboard.current.tKey.wasPressedThisFrame)
        {
            hasTeleporter = false;
            GameManager.Instance.DisplayMessage("Teleporting", 1f, 48);
            GameManager.Instance.TeleportPlayer();
        }

    }
    public override void Die()
    {
        // Send out Death event to GameManager
        GameManager.Instance.OnPlayerDeath();
    }
    public void EnableTeleport()
    {
        hasTeleporter = true;
    }


    // Overriding Takedamage to send out event whenever player receives damage
    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        GameManager.Instance.OnPlayerDamage(HP, maxHP);
    }

    // Pick up XP
    public void PickUpXp()
    {
        // Detects all objects will colliders that are in xp layer and add them to pickupXps array
        Collider2D[] pickupXps = Physics2D.OverlapCircleAll(transform.position, xpPickUpRadius, xpLayer);

        // Get each xp object and add the xp amount to player's xp, updating xp bar through GameManager
        foreach (Collider2D collider in pickupXps)
        {
            XpScript xp = collider.GetComponent<XpScript>();
            if (xp != null)
            {
                Debug.Log("Picked up " + xp.GetXpAmount() + " xp");
                xpAmount += xp.PickUpXp();
                Debug.Log("Current xp: " + xpAmount);
                GameManager.Instance.UpdateXp(xpAmount, xpToNextLevel);
            }
        }
    }

    public void LevelUp()
    {
        xpAmount -= xpToNextLevel;
        level++;
        isLevelingUp = true;
        LevelManager.Instance.LevelUp();
        UpdateXpToNextLevel();
        GameManager.Instance.UpdateXp(xpAmount, xpToNextLevel);
        GameManager.Instance.ShowUpgradeScreen(OnUpgradeComplete);
    }

    // Called when player finished choosing an upgrade and closing the upgrade screen 
    public void OnUpgradeComplete()
    {
        isLevelingUp = false;
    }

    public void UpdateXpToNextLevel() // scales positively with current player level
    {
        // Temporary formula 
        xpToNextLevel *= 1.5f;
    }

    // UnlockOrUpgradeAttack will check if the attack is already unlocked:
    // if not unlocked, then unlock it;
    // if unlocked, upgrades the attack tier (capped to maxTier)
    public void UnlockOrUpgradeAttack(string attackName)
    {
        if (allAttacks.TryGetValue(attackName, out AutoAttack attack))
        {
            if (!attack.gameObject.activeSelf)
            {
                attack.gameObject.SetActive(true);
                activeAttacks.Add(attack);
            }
            attack.UpgradeAttack();
        }
    }

}  
