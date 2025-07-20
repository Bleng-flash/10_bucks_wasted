using UnityEngine;

// the base Upgrade class that all concrete upgrades will inherit from

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/New Upgrade")]
public abstract class Upgrade : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite icon;
    public float weight = 1f; // default likelihood weight = 1, weight >= 0 for all upgrades 
                              // weight is set in inspector, and never mutated at runtime

    [SerializeField] private int applyCount = 0; // number of times this upgrade has been applied
    [SerializeField] private int maxApplyCount = -1; // -1 means upgrade can be applied unlimited times 
    private bool selectable = true; // indicates if this upgrade can be offered to player at level up

    public bool isSelectable()
    {
        if (maxApplyCount == -1) return true;
        return applyCount < maxApplyCount;
    }

    // Called when player selects the upgrade
    public virtual void ApplyUpgrade(PlayerScript player)
    {
        Debug.Log($"Applied {upgradeName}");
        applyCount++;
        selectable = isSelectable();
    }

    // Called at the end of each run when we reset 
    public void ResetUpgrade()
    {
        applyCount = 0;
        selectable = true;
    }

    // Expose selectable and applyCount for getters
    public bool Selectable => selectable;
    public int ApplyCount => applyCount;
}
