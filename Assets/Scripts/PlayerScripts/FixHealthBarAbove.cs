using UnityEngine;

public class FixHealthBarAbove : MonoBehaviour
{
    // LateUpdate to ensure this runs after all movement/rotations
    void LateUpdate()
    {
        // Stop health bar from rotating with player
        transform.rotation = Quaternion.identity;
    }
}
