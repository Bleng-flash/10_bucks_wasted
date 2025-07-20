using UnityEngine;
using UnityEngine.InputSystem;

// TESTING
// Solely used for testing; disable this component before pushing the game out
// Can only be used in scenes with a StageManager (cannot be used in Startscene)
public class SkipStageOrScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // press 'S' key to jump to Chapter1-Boss
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            if (GameManager.Instance.stageManager != null)
            {
                GameManager.Instance.stageManager.ProceedToNextStage(GameManager.Instance.sceneSwitcher);
            }
            else
            {
                Debug.Log("The current scene does not contain a StageManager");
            }
        }
    }
}

