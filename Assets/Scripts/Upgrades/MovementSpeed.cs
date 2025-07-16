using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseMovementSpeed", menuName = "Upgrades/Movement Speed")]
// set maxApplyCount = 5;
public class MovementSpeed : Upgrade
{
    [SerializeField] private float speedIncrease = 1f;

    public override void ApplyUpgrade(PlayerScript player)
    {
        base.ApplyUpgrade(player);

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.IncreaseSpeed(speedIncrease);
            Debug.Log("Movement speed increased by " + speedIncrease);
        }
        else
        {
            Debug.LogWarning("PlayerMovement component not found on player.");
        }
    }
}

