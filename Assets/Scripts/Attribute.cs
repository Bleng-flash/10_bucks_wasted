using System;
using UnityEngine;

// the below line tells Unity to display and serialise the custom class in the inspector
// aka make the fields editable in Inspector
[System.Serializable]
public class Attribute
{
    [SerializeField] // makes the private field appear in Inspector
    protected float maxHP;
    [SerializeField]
    protected float HP;
    [SerializeField]
    protected float ATK;
    protected bool isDead;

    public void Initialise(float maxHP, float HP, float ATK)
    {
        this.maxHP = maxHP;
        this.HP = HP;
        this.ATK = ATK;
        this.isDead = false;
    }

    public void IncreaseHealthBy(float inc)
    {
        this.HP += inc;
    }
    public void DecreaseHealthBy(float dec)
    {
        this.HP = Mathf.Max(0, this.HP - dec);      // Ensure minimum is 0
        Debug.Log("Current HP: " + this.HP);
        CheckDeath();
    }
    public void RestoreAllHealth()
    {
        this.HP = this.maxHP;
    }
    public void SetMaxHealthTo(float newMaxHP)
    {
        this.maxHP = newMaxHP;
    }
    public void SetAttackTo(float newATK)
    {
        this.ATK = newATK;
    }

    // returns true iff dead 
    public bool CheckDeath()
    {
        if (this.HP <= 0.0f)
        {
            this.isDead = true;
            return true;
        }
        return false;
    }

}
