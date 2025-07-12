using UnityEngine;

public class TailWhipOverlay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    void Awake()
    {
        sprite.enabled = false;
    }

    // Hides SpriteRenderer once animation finishes playing
    public void StopAnimation()
    {
        sprite.enabled = false;
    }

    // Turns on SpriteRenderer in preparation for animation
    public void PrepareAnimation()
    {
        if (sprite != null)
        {
            sprite.enabled = true;
            Debug.Log("PrepareFieldAnimation: sprite.enabled = " + sprite.enabled);
        }
        else
        {
            Debug.Log("No sprite detected!");
        }
    }
}
