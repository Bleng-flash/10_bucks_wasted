using UnityEngine;
using System.Collections.Generic;
using System.Xml.XPath;

public class StageManager : MonoBehaviour
{
    public enum Scene
    {
        Start,
        Chapter1Wave,
        Chapter1Boss,
        Chapter2Wave,
        Chapter2Boss,
        Win,
        GameOver
    }

    [SerializeField] private Scene sceneType;
    [SerializeField] private List<StageData> stages; // for wave scenes only
    [SerializeField] private List<EnemySpawning> spawners; // for wave scenes only
    private int currentStageIndex;
    [SerializeField] private GameObject teleporterPrefab;
    private GameObject currentTeleporter;
    public GameObject player;
    private float stageWidth = 60f;
    private float stageHeight = 40f;

    void Start()
    {
        currentStageIndex = 0;
        LoadStage(currentStageIndex);
    }

    private void LoadStage(int index)
    {
        ClearAllEnemies();

        if (currentTeleporter != null)
        {
            Destroy(currentTeleporter);
        }

        if (sceneType == Scene.Chapter1Wave || sceneType == Scene.Chapter2Wave)
        {
            if (index >= stages.Count)
            {
                Debug.Log("Invalid index to load stage");
                return;
            }
            StageData stageData = stages[index];
            ConfigureEnemySpawning(stageData.spawnCounts, stageData.spawnIntervals,
                stageData.enemyHealthMultiplier, stageData.enemyAttackMultiplier,
                stageData.XPDropMultiplier, stageData.scoreMultiplier);
            DisplayStageName(stageData.stageName);
            ConfigureTeleporter(stageWidth, stageHeight);
            TeleportPlayer(stageWidth, stageHeight);
        }
        CameraFollow cameraFollow = FindAnyObjectByType<CameraFollow>();
        if (cameraFollow != null)
        {
            cameraFollow.target = GameManager.Instance.player.transform;
        }

    }
    private void ClearAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    private void ConfigureEnemySpawning(List<int> spawnCounts, List<float> spawnIntervals,
            float enemyHealthMultiplier, float enemyAttackMultipler,
            float XPDropMultiplier, float scoreMultiplier)
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
            spawner.SetXPMultiplier(XPDropMultiplier);
            spawner.SetScoreMultiplier(scoreMultiplier);
        }
    }

    private void DisplayStageName(string name)
    {
        GameManager.Instance.DisplayMessage(name, 3f, 60);
    }

    // spawns a Teleporter gameobject somewhere within the boundary
    private void ConfigureTeleporter(float width, float height)
    {
        if (currentTeleporter != null)
        {
            Destroy(currentTeleporter);
        }
        width -= 0.5f; // don't spawn on border
        height -= 0.5f;
        float xPos = Random.Range(-width / 2, width / 2);
        float yPos = Random.Range(-height / 2, height / 2);
        Vector2 spawnPos = new Vector2(xPos, yPos);
        currentTeleporter = Instantiate(teleporterPrefab, spawnPos, Quaternion.identity);
    }

    // Spawns player somewhere within the boundary
    private void TeleportPlayer(float width, float height)
    {
        width -= 0.5f; // don't spawn on border
        height -= 0.5f;
        float xPos = Random.Range(-width / 2, width / 2);
        float yPos = Random.Range(-height / 2, height / 2);
        Vector2 spawnPos = new Vector2(xPos, yPos);
        player.transform.position = spawnPos;
    }

    public void ProceedToNextStage(SwitchScene sceneSwitcher)
    {
        currentStageIndex++;
        if (currentStageIndex >= stages.Count)
        {
            switch (sceneType)
            {
                case Scene.Chapter1Wave:
                    sceneSwitcher.LoadChapter1Boss();
                    break;
                case Scene.Chapter1Boss:
                    sceneSwitcher.LoadWinScene();
                    break;
                /*
                case Scene.Chapter2Wave:
                    sceneSwitcher.LoadChapter2Boss();
                    break;
                case Scene.Chapter2Boss:
                    sceneSwitcher.LoadWinScene();
                    break;
                */
                default:
                    sceneSwitcher.LoadGameOverScene();
                    break;
            }
        }
        else
        {
            LoadStage(currentStageIndex);
        }
    }
}
