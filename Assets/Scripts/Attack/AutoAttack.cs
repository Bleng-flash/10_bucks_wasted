using UnityEngine;

public abstract class AutoAttack : Attack
{

    // DO NOT provide an implementation for Update(), game will autocall Attack's implementation

    protected override bool TargetInRange()
    {
        return true;
    }
}
