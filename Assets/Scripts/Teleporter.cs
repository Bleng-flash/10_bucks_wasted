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
                GameManager.Instance.DisplayMessage("Picked up teleporter, press T to use", 2f, 40);
                player.EnableTeleport();  // Grants player teleport ability
                Destroy(gameObject);      // Remove teleporter GameObject
            }
        }
    }
}
