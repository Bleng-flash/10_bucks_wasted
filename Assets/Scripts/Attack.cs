using Unity.VisualScripting;
using UnityEngine;

/* 
Attack is an abstract class which specific attack scripts will inherit from.
An Attack can only be called on a Entity and be dealt to another Entity.
- I need to separate Attacks into auto-attacks (attacks regardless of whether there is a target
    in range), and those that are not; aka requires hitbox collisiions (target in range)
- Enemy attacks should all be single target. Enemies should not use auto-attack.
- Player attacks can be single target or AOE. Player should use auto attack for AOE attacks only.
*/
public abstract class Attack : MonoBehaviour
{
    protected float cooldown = 2.0f; // in seconds
    protected float damage = 10.0f;
    protected float timeSinceLastAttack = 0.0f; // time from last attack to now (current frame)
    protected bool autoAttack;

    public Attack(float cooldown, float dmg, bool autoAttack)
    {
        this.cooldown = cooldown;
        this.damage = dmg;
        this.autoAttack = autoAttack;
    }
    public void SetDamage(float dmg)
    {
        this.damage = dmg;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (CooldownOver() && CanAttack())
        {
            PerformAttack();
            timeSinceLastAttack = 0.0f;
        }
    }

    // PerformAttack() 
    protected abstract void PerformAttack();

    // CanAttack() will return always true for auto-attacks, and
    // it returns true for non auto-attacks iff there is a target in range
    protected abstract bool CanAttack();

    private bool CooldownOver()
    {
        return timeSinceLastAttack >= cooldown;
    }

}
