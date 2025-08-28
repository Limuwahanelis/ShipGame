using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    [Header("Debug"), SerializeField] bool _printState;
    [SerializeField] protected bool _debug;
    public GameObject MainBody => _mainBody;
    [SerializeField] protected EnemyStats _stats;
    [Header("Enemy common"), SerializeField] protected AnimationManager _enemyAnimationManager;
    [SerializeField] protected Transform _playerTransform;
    [SerializeField] protected Rigidbody2D _playerRB;
    [SerializeField] protected GameObject _mainBody;
    [SerializeField] protected StandardHealthBar _healthBar;
    [SerializeField] protected GunsComponent _guns;
    protected Dictionary<Type, EnemyState> _enemyStates = new Dictionary<Type, EnemyState>();
    protected EnemyState _currentEnemyState;
    public virtual void Awake()
    {

        if (_playerTransform == null) _playerTransform = FindFirstObjectByType<PlayerController>().MainBody.transform;
    }
    private void Start()
    {
        _healthBar.SetMaxHealth(_stats.MaxHP);
        _healthBar.AdjustForLength();
    }
    public EnemyState GetState(Type state)
    {
        return _enemyStates[state];
    }
    public virtual void Update()
    {
        if (PauseSettings.IsGamePaused) return;
        _currentEnemyState.Update();
    }
    public virtual void FixedUpdate()
    {
        if (PauseSettings.IsGamePaused) return;
        _currentEnemyState.FixedUpdate();
    }
    public void ChangeState(EnemyState newState)
    {
        if (_printState) Logger.Log(newState.GetType());
        _currentEnemyState.InterruptState();
        _currentEnemyState = newState;
    }
    public Coroutine WaitFrameAndExecuteFunction(Action function)
    {
        return StartCoroutine(WaitFrame(function));
    }
    public IEnumerator WaitFrame(Action function)
    {
        yield return new WaitForNextFrameUnit();
        function();
    }
}
