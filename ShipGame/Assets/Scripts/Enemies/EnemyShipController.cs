using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShipController : EnemyController
{


    [SerializeField] NavMeshAgent _agent;
    [SerializeField] ShipRaycasts _raycast;
    [SerializeField] SpawnableItem _spawnableItem;
    [SerializeField] PlayerMovement2D _playerMopvement;
    [SerializeField] GameEventIntSO _OnEnemyShipDestroyed;
    [SerializeField] int _lootFromShip;
    [SerializeField] Transform _healthBarTran;
    private ItemSpawner _cannonBallSpawner;
    protected EnemyShipContext _context;
    private bool _isInitialized=false;
    void Start()
    {
        if(_activateOnStart)
        {
            Initialize();
        }


    }
    private void OnDeath(IDamagable damagable)
    {
        _OnEnemyShipDestroyed?.Raise(_lootFromShip);
        _spawnableItem.ReturnToPool();
    }
    public void SetUp(Vector3 pos, PlayerMovement2D playerMopvement,Transform playerMainBody,ItemSpawner cannonBallSpawner)
    {
        _healthSystem.SetMacHP(_stats.MaxHP);
        _healthSystem.SetHP(_stats.MaxHP);
        _healthBar.SetMaxHealth(_stats.MaxHP);
        _healthBar.AdjustForLength();
        transform.position = pos;
        _mainBody.transform.position = transform.position;
        _healthBar.transform.position = _healthBarTran.position;
        _healthBar.GetComponent<FollowObject>().UpdateOffset();
        _playerTransform = playerMainBody;
        _playerMopvement = playerMopvement;
        _cannonBallSpawner = cannonBallSpawner;
        if (_isInitialized)
        {
            EnemyState newState = GetState(EnemyShipStateChase.StateType);
            ChangeState(newState);
            newState.SetUpState(_context);
        }
        else Initialize();
    }
    public void Initialize()
    {
        _isInitialized = true;

        _healthSystem.SetMacHP(_stats.MaxHP);
        _healthBar.SetMaxHealth(_stats.MaxHP);
        _healthBar.AdjustForLength();

        _agent.speed = _stats.Speed;
        _healthSystem.OnDeath += OnDeath;
        List<Type> states = AppDomain.CurrentDomain.GetAssemblies().SelectMany(domainAssembly => domainAssembly.GetTypes())
    .Where(type => typeof(EnemyState).IsAssignableFrom(type) && !type.IsAbstract).ToArray().ToList();

        _agent.updateRotation = false;
        _guns.SetUp(_stats.CannonsRange, _stats.CannonBallSpeed, _stats.CannonsDamage,_cannonBallSpawner);
        _context = new EnemyShipContext
        {
            ChangeEnemyState = ChangeState,
            animMan = _enemyAnimationManager,
            enemyTran = _mainBody.transform,
            playerTran = _playerTransform,
            stats = _stats,
            guns = _guns,
            agent = _agent,
            shipRaycasts = _raycast,
            playerMovement2D = _playerMopvement,
        };
        EnemyState.GetState getState = GetState;
        foreach (Type state in states)
        {
            _enemyStates.Add(state, (EnemyState)Activator.CreateInstance(state, getState));
        }

        // Set Startitng state
        EnemyState newState = GetState(EnemyShipStateChase.StateType);
        newState.SetUpState(_context);
        _currentEnemyState = newState;
        Logger.Log(newState.GetType());
    }
    private void OnDrawGizmos()
    {
        if(_debug)
        {
            if (_playerTransform == null || _stats==null) return;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_playerTransform.position, _stats.StartCirclingDistance);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_mainBody.transform.position, _stats.CannonsRange);

            if (_context == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_context.circlingMiddlePoint, _stats.StartCirclingDistance);

        }
    }
    private void OnDestroy()
    {
        _healthSystem.OnDeath -= OnDeath;
    }
}
