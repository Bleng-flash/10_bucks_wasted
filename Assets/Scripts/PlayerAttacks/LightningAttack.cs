using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

// Lightning Attack is a spawner script for the lightning prefabs, and can be considered the actual attack
public class LightningAttack : AutoAttack
{
    [SerializeField] private GameObject lightningStrikePrefab;
    [SerializeField] private int strikeCount = 3;
    [SerializeField] private float warningTime = 1f;    // If we want to implement a red circle to mark out strike area
    private float stageWidth = 60f;
    private float stageHeight = 40f;

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
            StartCoroutine(SpawnLightning(strikePosition));
        }
    }

    private IEnumerator SpawnLightning(Vector2 position)
    {
        // If we want to implement warning area, add it above here
        yield return new WaitForSeconds(warningTime);

        GameObject lightningStrike = Instantiate(lightningStrikePrefab, position, Quaternion.identity);
        LightningStrike strike = lightningStrike.GetComponent<LightningStrike>();
        strike.Damage = damage;
        strike.TargetLayer = targetLayer;
    }

    private Vector2 GetRandomPositionOnMap(float width, float height)
    {
        float xPos = Random.Range(-width / 2, width / 2);
        float yPos = Random.Range(-height / 2, height / 2);
        return new Vector2(xPos, yPos);
    }

    public override void Recalculate()
    {
        return;
    }
}
