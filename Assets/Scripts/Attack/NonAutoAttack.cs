using UnityEngine;

public abstract class NonAutoAttack : Attack
{
    // DO NOT provide an implementation for Update(), game will autocall Attack's implementation

    // All enemy attacks inherit from NonAutoAttack, implements TargetInRange() method.
}