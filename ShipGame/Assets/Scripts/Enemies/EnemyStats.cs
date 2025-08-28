using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float Speed => _speed;
    public float StartCirclingDistance { get => _startCirclingDistance; }
    public float EndCirclingDistance { get => _endCirclingDistance;  }
    public int MaxHP { get => _maxHP; }
    public float CannonsRange { get => _cannonsRange; }
    public int CannonsDamage { get => _cannonsDamage;  }
    public float FireCooldown { get => _fireCooldown; }
    public float CannonBallSpeed { get => _cannonBallSpeed; }

    [SerializeField] float _speed;
    [SerializeField] float _startCirclingDistance;
    [SerializeField] float _endCirclingDistance;
    [SerializeField] int _maxHP;
    [SerializeField] float _cannonsRange=8;
    [SerializeField] int _cannonsDamage=2;
    [SerializeField] float _fireCooldown=1f;
    [SerializeField] float _cannonBallSpeed=5f;
}
