using UnityEngine;

public class PeashooterAttack : NonAutoAttack
{
    [SerializeField] private float attackRadius = 10f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private BulletPool peaBulletPool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask borderLayer;

    void Start()
    {
        Initialise(this.cooldown, this.damage);
    }
    protected override void PerformAttack()
    {
        GameObject peaBulletObj = peaBulletPool.GetBullet();
        peaBulletObj.transform.position = firePoint.position;
        PeaBullet peaBullet = peaBulletObj.GetComponent<PeaBullet>();
        peaBullet.Speed = speed;
        peaBullet.Damage = damage;
        peaBullet.EnemyLayer = targetLayer;
        peaBullet.Pool = peaBulletPool;
        peaBullet.BorderLayer = borderLayer;
    }

    protected override bool TargetInRange()
    {
        Collider2D playerObject = Physics2D.OverlapCircle(owner.transform.position, attackRadius, targetLayer);
        return playerObject != null;
    }
    
    public override void Recalculate()
    {
        base.Recalculate();
        damage = Mathf.Max(0, owner.getATK());
    }
}
