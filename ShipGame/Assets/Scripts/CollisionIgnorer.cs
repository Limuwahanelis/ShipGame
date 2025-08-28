using UnityEngine;

public class CollisionIgnorer : MonoBehaviour
{
    [SerializeField] Collider2D _colliderToignoreCollisonWith;

    public void IgnoreColloisons(Collider2D col)
    {
        Physics2D.IgnoreCollision(_colliderToignoreCollisonWith, col, true);
    }
    public void RestoreColloisons(Collider2D col)
    {
        Physics2D.IgnoreCollision(_colliderToignoreCollisonWith, col, true);
    }
}
