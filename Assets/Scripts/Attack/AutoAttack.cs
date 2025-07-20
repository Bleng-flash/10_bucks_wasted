using UnityEngine;

public abstract class AutoAttack : Attack
{

    // DO NOT provide an implementation for Update(), game will autocall Attack's implementation

    // Player functionality
    public Upgrade upgradeData;
    
    protected override bool TargetInRange()
    {
        return true;
    }

    public abstract void UpgradeAttack(); 

}
