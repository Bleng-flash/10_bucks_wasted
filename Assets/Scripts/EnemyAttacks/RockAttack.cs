using System.Collections;
using UnityEngine;

public class RockAttack : NonAutoAttack
{
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private int strikeCount = 20;
    [SerializeField] private float warningTime = 1f;    // If we want to implement a red circle to mark out strike area
    private float stageWidth = 20f;
    private float stageHeight = 20f;
    void Start()
    {
        Initialise(this.cooldown, this.damage);
    }

    // Creates lightning prefabs at random locations
    protected override void PerformAttack()
    {
        for (int i = 0; i < strikeCount; i++)
        {
            Vector2 strikePosition = GetRandomPositionOnMap(stageWidth, stageHeight);
            StartCoroutine(SpawnRock(strikePosition));
        }
    }

    private IEnumerator SpawnRock(Vector2 position)
    {
        // If we want to implement warning area, add it above here
        yield return new WaitForSeconds(warningTime);

        GameObject rockStrike = Instantiate(rockPrefab, position, Quaternion.identity);
        RockStrike strike = rockStrike.GetComponentInChildren<RockStrike>();
        strike.Damage = damage;
        strike.TargetLayer = targetLayer;
    }

    private Vector2 GetRandomPositionOnMap(float width, float height)
    {
        float xPos = Random.Range(-width / 2, width / 2);
        float yPos = Random.Range(-height / 2, height / 2);
        return new Vector2(xPos, yPos);
    }

    protected override bool TargetInRange()
    {
        return true;
    }
}
