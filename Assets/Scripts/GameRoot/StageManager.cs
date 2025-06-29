using UnityEngine;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{

    [SerializeField] private List<StageData> stages;
    [SerializeField] private List<EnemySpawning> spawners;
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
        StageData stageData = stages[index];
        ConfigureEnemySpawning(stageData.spawnCounts, stageData.spawnIntervals,
            stageData.enemyHealthMultiplier, stageData.enemyHealthMultiplier);
        DisplayStageName();
        ConfigureTeleporter(60f, 40f);
    }
    private void ConfigureEnemySpawning(List<int> spawnCounts, List<float> spawnIntervals,
            float enemyHealthMultiplier, float enemyAttackMultipler)
    {
        if (spawnCounts.Count != spawners.Count || spawnIntervals.Count != spawners.Count)
        {
            Debug.Log("Different number of assigned spawners and spawnCounts/spawnIntervals");
            return;
        }
        for (int i = 0; i < spawners.Count; i++)
        {
            EnemySpawning spawner = spawners[i];
            spawner.SetSpawnCount(spawnCounts[i]);
            spawner.SetSpawnInterval(spawnIntervals[i]);
            spawner.SetHPMultiplier(enemyHealthMultiplier);
            spawner.SetATKMultiplier(enemyAttackMultipler);

        }

    }

    private void DisplayStageName()
    {
        
    }
    
    // spawns a Teleporter gameobject somewhere within the boundary
    private void ConfigureTeleporter(float width, float height)
    {
        width -= 0.5f; // don't spawn on border
        height -= 0.5f;
        float xPos = Random.Range(-width / 2, width / 2);
        float yPos = Random.Range(height / 2, height / 2);
        Vector2 spawnPos = new Vector2(xPos, yPos);
        GameObject teleporter = Instantiate(teleporterPrefab, spawnPos, Quaternion.identity);
    }

    public void ProceedToNextStage(SwitchScene sceneSwitcher)
    {
        currentStageIndex++;
        if (currentStageIndex >= stages.Count)
        {
            sceneSwitcher.LoadNextScene(); // change scene
            // initialise scenemanager upon loading new scene 
        }
        else
        {
            LoadStage(currentStageIndex); // same scene
        }
    }
}
