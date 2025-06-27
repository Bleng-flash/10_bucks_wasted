using UnityEngine;

// the base Upgrade class that all concrete upgrades will inherit from

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/New Upgrade")]
public abstract class Upgrade : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite icon;

    // Called when player selects the upgrade
    public virtual void ApplyUpgrade(PlayerScript player)
    {
        Debug.Log($"Applied {upgradeName}");
    }
}
