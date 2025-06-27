using UnityEngine;

// the base Upgrade class that all concrete upgrades will inherit from

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/New Upgrade")]
public abstract class Upgrade : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite icon;
    public float weight = 1f; // default likelihood weight, minimum value: set to 0 (do not set negative!)
    // we use weight to set the relative likelihoods of Upgrade assets being displayed to players

    // Called when player selects the upgrade
    public virtual void ApplyUpgrade(PlayerScript player)
    {
        Debug.Log($"Applied {upgradeName}");
    }
}
