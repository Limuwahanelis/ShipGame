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
        _context.gunsTimer += Time.deltaTime;
        _context.gunsTimer = Math.Clamp(_context.gunsTimer, 0, 60f);
        if (Vector2.Distance(_context.playeryPos, _context.enemyPos) <= _context.stats.CannonsRange)
        {
            if (_context.gunsTimer >= _context.stats.FireCooldown)
            {
                if (Vector2.Distance(_context.playerTran.position, _context.enemyPos) <= _context.stats.CannonsRange)
                {
                    _context.guns.LookAt(_context.playerTran.position + _context.playerTran.up * _context.playerMovement2D.Speed);
                    _context.gunsTimer = 0;
                    _context.guns.FireGuns();
                }
            }
        }
        if(Vector2.Distance(_context.enemyTran.position,_context.playerTran.position)<_context.stats.StartCirclingDistance)
        {
            _context.circlingMiddlePoint = _context.playerTran.position;
            ChangeState(EnemyShipStateCircling.StateType);
        }
        else
        {
            //_context.enemyTran.up = (_context.playeryPos - (Vector2)_context.enemyTran.position).normalized;
             _context.enemyTran.up = (_context.agent.steeringTarget - _context.enemyTran.position).normalized;
            _context.agent.SetDestination(_context.playerTran.position);
            _context.enemyTran.position += _context.enemyTran.up * _context.stats.Speed * Time.deltaTime;
        }
    }

    public override void SetUpState(EnemyContext context)
    {
        base.SetUpState(context);
        _context = (EnemyShipContext)context;
        //_context.agent.SetDestination(_context.playerTran.position);
        _context.agent.enabled = true;
        _context.agent.isStopped = true;
        _context.agent.updateRotation = true;
    }

    public override void InterruptState()
    {
     
    }
}