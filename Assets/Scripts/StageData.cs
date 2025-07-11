using UnityEngine;
using System.Collections.Generic;


public class StageData : ScriptableObject
{
    public string stageName;
    // Unity does not serialise dictionaries 
    public List<int> spawnCounts;
    public List<float> spawnIntervals;
    public float enemyHealthMultiplier = 1f;
    public float enemyAttackMultiplier = 1f;
    public float XPDropMultiplier = 1f;
    public float scoreMultiplier = 1f;
}

