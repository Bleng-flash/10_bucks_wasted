using UnityEngine;

public class UpgradeUICanvasPersist : MonoBehaviour
{
    private static UpgradeUICanvasPersist instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // prevent duplicates
        }
    }
}
