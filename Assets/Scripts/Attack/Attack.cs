using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

/* 
Attack is an abstract class which specific attack scripts will inherit from.
At runtime, an Attack will be attached to a GameObject that is a child of an Entity GameObject.
We separate Attacks into auto-attacks (attacks regardless of whether there is a target
    in range), and those that are not; aka requires hitbox collisiions (target in range)
- Enemies should use non-auto-attack.
- Player should use auto attack.
*/
[System.Serializable]
public abstract class Attack : MonoBehaviour
{
    [SerializeField] protected float cooldown; // in seconds
    [SerializeField] protected float damage;
    protected float timeSinceLastAttack = 0.0f; // time from last attack to now (current frame)
    protected Entity owner; // the entity that this attack belongs to 
    [SerializeField] protected LayerMask targetLayer; // the layer of the targets that the attack can hit


    // We call Initialise on attacks at runtime in the Start() method of the concrete attacks
    protected void Initialise(float cooldown, float damage)
    {
        this.cooldown = cooldown;
        this.damage = damage;
        owner = GetComponentInParent<Entity>(); // checks for script that is subclass of Entity
        if (owner == null)
        {
            Debug.LogError("Owner missing on GameObject.");
            return;
        }
        SetTargetLayer();
    }

    protected void SetTargetLayer()
    {
        if (owner is PlayerScript)
        {
            targetLayer = LayerMask.GetMask("Enemy");
        }
        if (owner is EnemyScript)
        {
            targetLayer = LayerMask.GetMask("Player");
        }
    }

    public void SetDamage(float dmg)
    {
        this.damage = dmg;
    }

    // Update is called once per frame
    // DO NOT provide implementation for Update() in any subclasses of Attack
    protected void Update()
    {
        // Stops all attacks once player is dead
        if (!GameManager.Instance.isPlayerAlive)
        {
            return;
        }

        timeSinceLastAttack += Time.deltaTime;
        if (CooldownOver() && TargetInRange())
        {
            PerformAttack();
            timeSinceLastAttack = 0.0f;
        }

        // Call visual update if child class has it
        OnUpdate();
    }

    protected virtual void OnUpdate() { }

    // PerformAttack() -- we only provide this implementation in concrete attack scripts
    protected abstract void PerformAttack();

    // TargetInRange will always return true for auto-attacks, 
    // and depends on implementation for non-auto attacks
    protected abstract bool TargetInRange();

    private bool CooldownOver()
    {
        return timeSinceLastAttack >= cooldown;
    }

}
