using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipStateCircling : EnemyState
{
    public static Type StateType { get => typeof(EnemyShipStateCircling); }
    private EnemyShipContext _context;
    private Vector2 _previousPos;
    private Vector2 _targetPos;
    private float _angle;
    private float _angularSpeed;
    public EnemyShipStateCircling(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        
        if (Vector2.Distance(_context.circlingMiddlePoint, _context.playerTran.position) > _context.stats.StartCirclingDistance)
        {
            ChangeState(EnemyShipStateChase.StateType);
        }
        else
        {

            if (Vector2.Distance(_context.enemyPos, _targetPos) > 0.02f)
            {
                _context.enemyPos = Vector2.MoveTowards(_context.enemyPos, _targetPos, _context.stats.Speed * Time.deltaTime);
                _context.enemyTran.up = _targetPos- _context.enemyPos;
                return;
            }
            else
            {
                
                _angle += _angularSpeed * Time.deltaTime;
                //Logger.Log(_angle * Mathf.Rad2Deg);
                _targetPos = new Vector2(Mathf.Sin(_angle) * _context.stats.StartCirclingDistance + _context.circlingMiddlePoint.x, Mathf.Cos(_angle) * _context.stats.StartCirclingDistance + _context.circlingMiddlePoint.y);
                _context.enemyPos = _targetPos;
                _previousPos = _context.enemyPos;
                _context.enemyTran.up = Vector2.Perpendicular(_context.circlingMiddlePoint - _context.enemyPos);
                if (_angle >= 2 * Mathf.PI) _angle = 0;
            }
        }
    }

    public override void SetUpState(EnemyContext context)
    {
        base.SetUpState(context);
        _context = (EnemyShipContext)context;
        _angle = MathF.Atan2(_context.enemyPos.y-_context.circlingMiddlePoint.y, -(_context.enemyPos.x - _context.circlingMiddlePoint.x))-MathF.PI/2;
        _angularSpeed = (_context.stats.Speed / _context.stats.StartCirclingDistance);
        _targetPos = new Vector2(Mathf.Sin(_angle) * _context.stats.StartCirclingDistance + _context.circlingMiddlePoint.x, Mathf.Cos(_angle) * _context.stats.StartCirclingDistance + _context.circlingMiddlePoint.y);
        _previousPos = _context.enemyPos;
    }

    private float Convert(float angleInDeg)
    {
        return Mathf.Deg2Rad * angleInDeg;
    }
    public override void InterruptState()
    {
     
    }
}