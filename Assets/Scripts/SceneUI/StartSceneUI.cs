using UnityEngine;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Start()
    {   
        if (GameManager.Instance.sceneSwitcher != null)
        {
            playButton.onClick.AddListener(GameManager.Instance.sceneSwitcher.LoadChapter1Wave);
        }
        else
        {
            Debug.LogError("sceneSwithcer is null.");
        }
    }
}
