using UnityEngine;

public class XpScript : MonoBehaviour
{
    private int xpAmount;

    public void SetXpAmount(int amount)
    {
        xpAmount = amount;
    }

    public int GetXpAmount()
    {
        return xpAmount;
    }
    public int PickUpXp()
    {
        Destroy(gameObject);
        return GetXpAmount();
    }
}
