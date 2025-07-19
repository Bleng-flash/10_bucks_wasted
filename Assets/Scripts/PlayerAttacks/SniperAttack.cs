using UnityEngine;

// SniperAttack draws bullets from bulletPool, considered actual attack
public class SniperAttack : AutoAttack
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int maxPierceCount = 1;
    private PlayerMovement playerMovement;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private Transform firePoint;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    protected override void PerformAttack()
    {
        GameObject bulletObj = bulletPool.GetBullet();
        bulletObj.transform.position = firePoint.position;

        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.Speed = speed;
        bullet.Damage = damage;
        bullet.MaxPierceCount = maxPierceCount;
        bullet.EnemyLayer = targetLayer;
        bullet.Direction = playerMovement.FacingDirection.normalized;
        bullet.Pool = bulletPool;
    }
}
