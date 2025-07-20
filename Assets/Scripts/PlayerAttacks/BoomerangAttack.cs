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
        return;
    }
    
    public override void UpgradeAttack()
    {
    }
}
