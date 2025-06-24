using UnityEngine;

public class ParentColliderToChild : MonoBehaviour
{
    private EnemyBashAttack bashAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bashAttack = GetComponentInChildren<EnemyBashAttack>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bashAttack?.OnParentCollisionEnter2D(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        bashAttack?.OnParentCollisionExit2D(collision);
    }
}
