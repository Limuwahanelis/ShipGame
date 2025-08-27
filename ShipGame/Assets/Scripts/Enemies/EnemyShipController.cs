using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyShipController : EnemyController
{
    
   
    protected EnemyShipContext _context;

    void Start()
    {
        List<Type> states = AppDomain.CurrentDomain.GetAssemblies().SelectMany(domainAssembly => domainAssembly.GetTypes())
    .Where(type => typeof(EnemyState).IsAssignableFrom(type) && !type.IsAbstract).ToArray().ToList();

        _context = new EnemyShipContext
        {
            ChangeEnemyState = ChangeState,
            animMan = _enemyAnimationManager,
            enemyTran = _mainBody.transform,
            playerTran = _playerTransform,
            stats = _stats,
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

            if (_context == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_context.circlingMiddlePoint, _stats.StartCirclingDistance);
        }
    }
}
