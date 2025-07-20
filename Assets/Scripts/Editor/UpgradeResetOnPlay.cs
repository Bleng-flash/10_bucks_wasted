using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

// TESTING script -- make my life easier by allowing repeated test runs

// This script must be placed in Assets/Editor to Unity treats it as an editor-only script
// It will only run in the Unity editor

// purpose of this script: resets all upgrades (weight and applyCount) whenever we enter/exit play mode
// Otherwise in the Unity editor the data of ScriptableObjects persist across exitting and entering
// play mode.

// This is not required for the actual game build
// UpgradeResetOnPlay is only needed to manually reset the ScriptableObject state when you 
// hit Play in the Unity Editor, because ScriptableObjects persist their state between Play Mode
// sessions in the editor. 
// That persistence does not happen in a game build -- each run starts fresh.
// For actual game build the method GameManager.ResetRun() should already do its job properly.

[InitializeOnLoad]
public static class UpgradeResetOnPlay
{
    static UpgradeResetOnPlay()
    {
        EditorApplication.playModeStateChanged += ResetUpgradesOnEnterPlayMode;
    }

    private static void ResetUpgradesOnEnterPlayMode(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode || state == PlayModeStateChange.ExitingPlayMode)
        {
            string[] upgradeGuids = AssetDatabase.FindAssets("t:Upgrade", new[] { "Assets/Upgrades" });
            foreach (string guid in upgradeGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Upgrade upgrade = AssetDatabase.LoadAssetAtPath<Upgrade>(path);

                if (upgrade != null)
                {
                    upgrade.ResetUpgrade();
                    EditorUtility.SetDirty(upgrade); // Marks object as modified
                }
            }

            Debug.Log("All upgrades in Assets/Upgrades have been reset.");
        }
    }
}

