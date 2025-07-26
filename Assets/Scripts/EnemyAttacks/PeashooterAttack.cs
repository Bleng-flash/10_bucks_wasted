using UnityEngine;

public class PeashooterAttack : NonAutoAttack
{
    [SerializeField] private float attackRadius = 10f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private BulletPool peaBulletPool;
    public BulletPool PeaBulletPool
    {
        get => peaBulletPool;
        set => peaBulletPool = value;
    }
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask borderLayer;
    private Transform player;

    void Start()
    {
        Initialise(this.cooldown, this.damage);
    }
    protected override void PerformAttack()
    {
        GameObject peaBulletObj = PeaBulletPool.GetBullet();
        peaBulletObj.transform.position = firePoint.position;

        Collider2D playerObject = Physics2D.OverlapCircle(owner.transform.position, attackRadius, targetLayer);
        player = playerObject.GetComponent<Transform>();
        Vector2 shootDir = (player.position - transform.position).normalized;

        PeaBullet peaBullet = peaBulletObj.GetComponent<PeaBullet>();
        peaBullet.Speed = speed;
        peaBullet.Damage = damage;
        peaBullet.EnemyLayer = targetLayer;
        peaBullet.Direction = shootDir;
        peaBullet.Pool = PeaBulletPool;
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
