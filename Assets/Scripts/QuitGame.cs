using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        #if UNITY_EDITOR
            // If running in the editor, stop play mode
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If running in a build, quit the application
            Application.Quit();
        #endif
    }
}
