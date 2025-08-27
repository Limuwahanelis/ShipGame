using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipStateChase : EnemyState
{
    public static Type StateType { get => typeof(EnemyShipStateChase); }
    private EnemyShipContext _context;
    public EnemyShipStateChase(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        if(Vector2.Distance(_context.enemyTran.position,_context.playerTran.position)<_context.stats.StartCirclingDistance)
        {
            _context.circlingMiddlePoint = _context.playerTran.position;
            ChangeState(EnemyShipStateCircling.StateType);
        }
        else
        {
            _context.enemyTran.up = (_context.playerTran.position - _context.enemyTran.position).normalized;
            _context.enemyTran.position += _context.enemyTran.up * _context.stats.Speed * Time.deltaTime;
        }
    }

    public override void SetUpState(EnemyContext context)
    {
        base.SetUpState(context);
        _context = (EnemyShipContext)context;
    }

    public override void InterruptState()
    {
     
    }
}