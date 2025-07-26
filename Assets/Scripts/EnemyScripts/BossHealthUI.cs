using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    public void UpdateBossHealth(float current, float max)
    {
        // Figure out percentage of health bar to scale horizontally to (ie if 80/100 will scale 0.8)
        float percent = Mathf.Clamp01(current / max);
        // Scale health bar horizontally
        healthBar.transform.localScale = Vector3.one;
        healthBar.transform.localScale = new Vector3(percent, 1f, 1f);  // arguments are x, y, and z axis scaling
    }
}
