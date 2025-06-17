using UnityEngine;

public class XpScript : MonoBehaviour
{
    // SerializeField is temperory for testing purposes
    [SerializeField] private int xpAmount;

    // Method should be called by enemies after they are killed
    public void SetXpAmount(int amount)
    {
        xpAmount = amount;
    }
}
