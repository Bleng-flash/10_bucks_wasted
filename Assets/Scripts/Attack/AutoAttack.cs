using UnityEngine;

public abstract class AutoAttack : Attack
{

    // DO NOT provide an implementation for Update(), game will autocall Attack's implementation

    // Player functionality
    public int tier = 0; // Attacks unlock at base tier of 1, when not unlocked tier = 0
    public int maxTier = 5;
    [SerializeField] private Upgrade upgradeData;

    protected override bool TargetInRange()
    {
        return true;
    }

    public virtual void UpgradeAttack()
    {
        tier++;
        if (tier >= maxTier && upgradeData != null)
        {
            upgradeData.weight = 0f; // Disable the upgrade option from appearing again
        }
    }

}
