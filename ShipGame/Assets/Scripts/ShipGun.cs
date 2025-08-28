using UnityEngine;

public class ShipGun : MonoBehaviour
{
    public SpriteRenderer RangeLine { get => _rangeLine; }
    public Transform CannonBallSpawnPoint { get => _cannonBallSpawnPoint;  }

    [SerializeField] SpriteRenderer _rangeLine;
    [SerializeField] Transform _cannonBallSpawnPoint;
    [SerializeField] CollisionIgnorer _collisionIgnorer;
    ItemSpawner _cannonBallSpawner;
    private float _cannonBallMaxDistance;
    private float _cannonBallSpeed;
    private int _cannonBallDamge;
    public void SetUp(ItemSpawner cannonBallSpawner, float travelRange, float cannonBallSpeed , int damage)
    {
        _rangeLine.transform.localPosition = new Vector3(0, travelRange / 2, 0);
        _rangeLine.transform.localScale = new Vector3( _rangeLine.transform.localScale.x, travelRange, _rangeLine.transform.localScale.z);
        _cannonBallSpawner = cannonBallSpawner;
        _cannonBallDamge = damage;
        _cannonBallMaxDistance = travelRange;
        _cannonBallSpeed = cannonBallSpeed;
    }
    public void Fire()
    {
        SpawnableItem item = _cannonBallSpawner.GetItem();
        CannonBall cannonBall = item.GetComponent<CannonBall>();
        item.SetActionToReturnToPool(cannonBall.ResetCannonBall);
        cannonBall.SetUp(_cannonBallMaxDistance, _cannonBallDamge,_cannonBallSpeed, transform.up,_collisionIgnorer);
        cannonBall.transform.position = _cannonBallSpawnPoint.position;
    }

}
