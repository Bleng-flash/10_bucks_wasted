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
}
