using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipStateCircling : EnemyState
{
    public static Type StateType { get => typeof(EnemyShipStateCircling); }
    private EnemyShipContext _context;
    private Vector2 _targetPos;
    private float _angle;
    private float _angularSpeed;
    private bool _gobackToCircle;

    public EnemyShipStateCircling(GetState function) : base(function)
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
        if (Vector2.Distance(_context.circlingMiddlePoint, _context.playerTran.position) > _context.stats.StartCirclingDistance)
        {
            ChangeState(EnemyShipStateChase.StateType);
        }
        else
        {

            if (_context.shipRaycasts.isHittingWall)
            {
                ChangeState(EnemyShipStateBackToCircle.StateType);
                return;
            }
            if (_gobackToCircle)
            {
                _context.enemyPos += Time.deltaTime * (Vector2)_context.enemyTran.up * _context.stats.Speed;
                if (Vector2.Distance(_targetPos, _context.enemyPos) <= 0.05f)
                {
                    _gobackToCircle = false;
                }
            }
            else
            {
                _angle += _angularSpeed * Time.deltaTime;
                _targetPos = new Vector2(Mathf.Sin(_angle) * _context.stats.StartCirclingDistance + _context.circlingMiddlePoint.x, Mathf.Cos(_angle) * _context.stats.StartCirclingDistance + _context.circlingMiddlePoint.y);
                _context.enemyPos = _targetPos;
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
        _context.agent.isStopped = true;
        _context.agent.updateRotation = false;
        _context.agent.enabled = false;
        _gobackToCircle = false;
        if (Vector2.Distance(_targetPos, _context.enemyPos) > 0.05f)
        {
            _gobackToCircle = true;
            _context.enemyTran.up = (_targetPos - _context.enemyPos).normalized;
        }

    }

    private float Convert(float angleInDeg)
    {
        return Mathf.Deg2Rad * angleInDeg;
    }
    public override void InterruptState()
    {
     
    }
}