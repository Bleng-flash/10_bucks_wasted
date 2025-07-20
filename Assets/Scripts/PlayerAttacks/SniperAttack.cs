using UnityEngine;

// SniperAttack draws bullets from bulletPool, considered actual attack
public class SniperAttack : AutoAttack
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int maxPierceCount = 1;
    private PlayerMovement playerMovement;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask borderLayer;
    private Vector2 originalOffset;
    private float damageMultiplier = 5f; // damage = damageMultiplier * player ATK


    void Start()
    {
        Initialise(this.cooldown, this.damage);
        playerMovement = GetComponentInParent<PlayerMovement>();
        originalOffset = transform.localPosition;
    }

    protected override void PerformAttack()
    {
        GameObject bulletObj = bulletPool.GetBullet();
        bulletObj.transform.position = firePoint.position;

        Vector2 shootDir = playerMovement.FacingDirection.normalized;

        // Rotate bullet sprite to face direction
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        bulletObj.transform.rotation = Quaternion.Euler(0, 0, angle);

        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.Speed = speed;
        bullet.Damage = damage;
        bullet.MaxPierceCount = maxPierceCount;
        bullet.EnemyLayer = targetLayer;
        bullet.Direction = shootDir;
        bullet.Pool = bulletPool;
        bullet.BorderLayer = borderLayer;
    }

   public override void Recalculate()
    {
        damage = Mathf.Max(0, damageMultiplier * owner.getATK());
    }

    public override void UpgradeAttack()
    {
        int tier = upgradeData.ApplyCount; // 0 to 5 (inclusive)

        switch (tier)
        {
            case 0:
                break;
            case 1: // base case
                damageMultiplier = 5f;
                cooldown = 4f;
                maxPierceCount = 1;
                break;
            case 2:
                damageMultiplier = 7f;
                maxPierceCount = 3;
                break;
            case 3:
                cooldown = 2f;
                break;
            case 4:
                damageMultiplier = 10f;
                maxPierceCount = 5;
                break;
            case 5:
                maxPierceCount = 7;
                break;
            default:
                Debug.LogWarning("Invalid value: applyCount for SniperAttack");
                break;
        }

        Recalculate();
    }
}
