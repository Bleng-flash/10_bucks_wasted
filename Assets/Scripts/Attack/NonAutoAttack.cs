using UnityEngine;

public abstract class NonAutoAttack : Attack
{
    // DO NOT provide an implementation for Update(), game will autocall Attack's implementation

    protected override bool TargetInRange()
    {
        return FindValidTarget() != null;
    }

    // We will use the field owner in Attack class and the method IsEnemyTo in Entity class
    protected abstract Entity FindValidTarget();

}