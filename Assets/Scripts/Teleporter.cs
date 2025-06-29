using UnityEngine;

public class Teleporter : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player != null)
            {
                player.EnableTeleport();  // Grants player teleport ability
                Destroy(gameObject);      // Remove teleporter GameObject
            }
        }
    }
}
