using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image xpBar;



    public void UpdateHealth(float current, float max)
    {
        healthBar.fillAmount = current / max;
    }

    public void UpdateXp(float current, float max)
    {
        xpBar.fillAmount = current / max;
    }
}
