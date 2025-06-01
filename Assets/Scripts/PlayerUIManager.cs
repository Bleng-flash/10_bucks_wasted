using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private PlayerScript player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthText.text = "Health: " + player.GetHealth();
    }
}
