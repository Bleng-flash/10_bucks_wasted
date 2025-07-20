using UnityEngine;

public class XpScript : MonoBehaviour
{
    private float xpAmount;
    [SerializeField] private LayerMask playerLayer;
    private bool isPickingUp = false;
    private Transform playerTransform;
    private float speed = 15f;

    void Update()
    {
        if (isPickingUp)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

    public void SetXpAmount(float amount)
    {   
        xpAmount = amount;
    }

    public void PickUpXp(Transform player)
    {
        isPickingUp = true;
        playerTransform = player;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsInLayerMask(other.gameObject, playerLayer))
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            player.AddXp(xpAmount);
            Destroy(gameObject);
        }
    }
    
    bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return ((1 << obj.layer) & mask) != 0;
    }
}
