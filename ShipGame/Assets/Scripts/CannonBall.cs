using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb2d;
    [SerializeField] SpawnableItem _spawnableItem;
    [SerializeField] Collider2D _col;
    private float _travelRange;
    private float _speed;
    private float _travelledDistance=0;
    private int _damage;
    private DamageInfo _damageInfo;
    private CollisionIgnorer _collisionIgnorer;
    private bool _hasRestoredCol=false;
    public void SetUp(float travelRange, int damge,float speed,Vector3 upVector, CollisionIgnorer collisionIgnorer)
    {
        _travelRange = travelRange;
        _damage = damge;
        _travelledDistance = 0;
        _speed = speed;
        _damageInfo = new DamageInfo(_damage, transform.position);
        transform.up = upVector;
        _collisionIgnorer = collisionIgnorer;
        _collisionIgnorer.IgnoreColloisons(_col);
        _hasRestoredCol = false;
    }
    private void FixedUpdate()
    {
        _rb2d.MovePosition(_rb2d.position + (Vector2)_rb2d.transform.up * _speed * Time.deltaTime);
        _travelledDistance += _speed * Time.deltaTime;
        if (_travelledDistance >= _travelRange)
        {
            _spawnableItem.ReturnToPool();
        }
        if (!_hasRestoredCol && _travelledDistance>1f)
        {
            _hasRestoredCol = true;
            _collisionIgnorer.RestoreColloisons(_col);
        }
        
    }
    public void ResetCannonBall()
    {
        _travelledDistance = 0;
        _hasRestoredCol = false;
    }
    public void DealDamage(Collider2D col)
    {
        _damageInfo.dmgPosition=transform.position;
        col.attachedRigidbody.GetComponent<HealthSystem>().TakeDamage(_damageInfo);
        _collisionIgnorer.RestoreColloisons(_col);
        _spawnableItem.ReturnToPool();

    }
}
