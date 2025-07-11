using UnityEngine;
using UnityEngine.InputSystem;

// Solely used for testing; disable this component before pushing the game out 
public class TestLevelUp : MonoBehaviour
{
    [SerializeField] private PlayerScript player; 
    void Update()
    {
        // Press 'L' key to level up
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            if (player != null)
            {
                player.LevelUp();
                Debug.Log("Forced level up triggered.");
            }

        }
    }
}
