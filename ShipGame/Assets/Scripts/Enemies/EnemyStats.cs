using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float Speed => _speed;

    public float StartCirclingDistance { get => _startCirclingDistance; }
    public float EndCirclingDistance { get => _endCirclingDistance;  }
    public int MaxHP { get => _maxHP; }

    [SerializeField] float _speed;
    [SerializeField] float _startCirclingDistance;
    [SerializeField] float _endCirclingDistance;
    [SerializeField] int _maxHP;
}
