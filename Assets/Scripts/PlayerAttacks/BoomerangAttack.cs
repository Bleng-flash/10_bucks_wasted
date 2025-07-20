using Unity.VisualScripting;
using UnityEngine;

// Script that spawns boomerang prefabs
public class BoomerangAttack : AutoAttack
{
    [SerializeField] private GameObject boomerangPrefab;
    [SerializeField] private int boomerangCount = 1;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float hitCooldown = 1f;
    [SerializeField] private float angleSpread = 40f;
    private PlayerMovement playerMovement;

    void Start()
    {
        Initialise(this.cooldown, this.damage);
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    protected override void PerformAttack()
    {
        Vector2 facingDirection = playerMovement.FacingDirection.normalized;

        for (int i = 0; i < boomerangCount; i++)
        {
            float angleOffset = (i - (boomerangCount - 1) / 2f) * angleSpread;
            Vector2 dir = Quaternion.Euler(0, 0, angleOffset) * facingDirection;

            GameObject obj = Instantiate(boomerangPrefab, transform.position, Quaternion.identity);
            BoomerangScript boomerang = obj.GetComponent<BoomerangScript>();
            boomerang.Speed = speed;
            boomerang.MaxDistance = maxDistance;
            boomerang.Damage = damage;
            boomerang.HitCooldown = hitCooldown;
            boomerang.EnemyLayer = targetLayer;
            boomerang.ThrowBoomerang(dir, transform);
        }
    }

    public override void Recalculate()
    {
        damage = Mathf.Max(0, 0.25f * owner.getATK()); 
    }

    public override void UpgradeAttack()
    {
        int tier = upgradeData.ApplyCount; // 0 to 5 (inclusive)

        switch (tier)
        {
            case 0:
                break;
            case 1: // base case
                boomerangCount = 1;
                cooldown = 5f;
                break;
            case 2:
                boomerangCount = 2;
                break;
            case 3:
                boomerangCount = 3;
                cooldown = 4f;
                break;
            case 4:
                boomerangCount = 4;
                break;
            case 5:
                boomerangCount = 5;
                cooldown = 3f;
                break;
            default:
                Debug.LogWarning("Invalid value: applyCount for BoomerangAttack");
                break;
        }

        Recalculate();
    }
}
