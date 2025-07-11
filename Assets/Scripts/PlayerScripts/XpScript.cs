using UnityEngine;

public class XpScript : MonoBehaviour
{
    private float xpAmount;

    public void SetXpAmount(float amount)
    {
        xpAmount = amount;
    }

    public float GetXpAmount()
    {
        return xpAmount;
    }
    public float PickUpXp()
    {
        Destroy(gameObject);
        return GetXpAmount();
    }
}
