using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("SampleScene"); // Match the scene name exactly
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene"); // match scene name exactly
    }
}
