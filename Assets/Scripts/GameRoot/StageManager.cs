using UnityEngine;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{

    [SerializeField] private List<StageData> stages;
    [SerializeField] private EnemySpawning spawner;
    private int currentStageIndex;
    [SerializeField] private GameObject teleporterPrefab;
    private GameObject currentTeleporter;

    void Start()
    {
        currentStageIndex = 0;
        LoadStage(currentStageIndex);
    }

    private void LoadStage(int index)
    {
        if (index >= stages.Count)
        {
            Debug.Log("Invalid index to load stage");
            return;
        }
        // configure spawner and teleporter
    }

    public void ProceedToNextStage(SwitchScene sceneSwitcher)
    {
        currentStageIndex++;
        if (currentStageIndex >= stages.Count)
        {
            sceneSwitcher.LoadNextScene(); // change scene
        }
        else
        {
            LoadStage(currentStageIndex); // same scene
        }
    }
}
