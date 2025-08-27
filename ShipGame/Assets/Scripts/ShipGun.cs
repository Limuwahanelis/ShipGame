using UnityEngine;

public class ShipGun : MonoBehaviour
{
    public SpriteRenderer RangeLine { get => _rangeLine; }
    public Transform CannonBallSpawnPoint { get => _cannonBallSpawnPoint;  }

    [SerializeField] SpriteRenderer _rangeLine;
    [SerializeField] Transform _cannonBallSpawnPoint;

}
