using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene"); // match scene name exactly
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }
    public void LoadChapter1Wave()
    {
        SceneManager.LoadScene("Chapter1-Wave");
    }

    public void LoadChapter1Boss()
    {
        SceneManager.LoadScene("Chapter1-Boss");
    }
    public void LoadChapter2Wave()
    {
        SceneManager.LoadScene("Chapter2-Wave");
    }
    public void LoadChapter2Boss()
    {
        SceneManager.LoadScene("Chapter2-Boss");
    }
    public void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes in build settings");
        }
    }
}
