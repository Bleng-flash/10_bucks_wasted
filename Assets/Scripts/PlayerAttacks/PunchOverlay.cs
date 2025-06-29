using UnityEngine;

public class PunchOverlay : MonoBehaviour
{
    private SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();   
    }

    // Hides SpriteRenderer once animation finishes playing
    public void StopSlashAnimation()
    {
        sprite.enabled = false;
    }

    // Turns on SpriteRenderer in preparation for animation
    public void PrepareSlashAnimation()
    {
        sprite.enabled = true;
    }
}
