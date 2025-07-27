using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public PlayerScript player;
    public Image healthBar;
    [SerializeField] private Image xpBar;



    public void UpdateHealth(float current, float max)
    {
        healthBar.fillAmount = current / max;
    }

    public void UpdateXp(float current, float max)
    {
        // Figure out percentage of xp bar to scale horizontally to (ie if 80/100 will scale 0.8)
        float percent = Mathf.Clamp01(current / max);
        // Scale xp bar horizontally
        xpBar.transform.localScale = new Vector3(percent, 1f, 1f);  // arguments are x, y, and z axis scaling
    }

    // To assign image in testing environment
    public void SetXpBar(Image image)
    {
        xpBar = image;
    }
}
