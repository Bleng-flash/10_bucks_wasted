using UnityEngine;

public class XpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject xpPrefab;

    // Method should be called by enemies after they are killed
    public void DropXp(int amount, Vector2 enemyPos)
    {
        GameObject xp = Instantiate(xpPrefab, enemyPos, Quaternion.identity);

        // Set XP amount to be dropped by killed enemy
        XpScript xpScript = xp.GetComponent<XpScript>();
        xpScript.SetXpAmount(amount);
    }
}
