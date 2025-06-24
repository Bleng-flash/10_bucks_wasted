using UnityEngine;

public abstract class AutoAttack : Attack
{

    // DO NOT provide an implementation for Update(), game will autocall Attack's implementation


    // CanAttack() will return always true for auto-attacks, and
    // it returns true for non auto-attacks iff there is a target in range
    protected override bool TargetInRange()
    {
        return true;
    }
}
