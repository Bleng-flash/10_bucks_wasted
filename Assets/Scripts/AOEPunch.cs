using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// Basic starting attack for player character
public class AOEPunch : Attack
{
    [SerializeField] private float AOEPunchCooldown = 2.0f; // in seconds

    [SerializeField] private float AOEPunchDamage = 10;
    private List<Collider2D> hitEnemies = new List<Collider2D>();

    void Start()
    {
        Initialise(AOEPunchCooldown, AOEPunchDamage, true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // For now we use Enemy tags, but see if there's a better way to classify enemies
        // that doesn't rely on tags
        if (collision.CompareTag("Enemy"))
        {
            hitEnemies.Add(collision);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        hitEnemies.Remove(collision);
    }

    // AOE Punch is an auto-attack so CanAttack is always true
    protected override bool CanAttack()
    {
        return true;
    }

    protected override void PerformAttack()
    {
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<PlayerScript>().TakeDamage(damage);
            Debug.Log("Take " + AOEPunchDamage + " damage");
        }
    }

}
