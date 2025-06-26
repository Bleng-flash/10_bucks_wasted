using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite icon;

    // Called when player selects the upgrade
    public abstract void ApplyUpgrade(PlayerScript player);
}
