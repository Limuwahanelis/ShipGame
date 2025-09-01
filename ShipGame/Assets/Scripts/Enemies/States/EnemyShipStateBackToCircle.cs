using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipStateBackToCircle : EnemyState
{
    public static Type StateType { get => typeof(EnemyShipStateBackToCircle); }
    private EnemyShipContext _context;
    private bool _isSwimmingAlongTheCircle = true;
    private Vector2 _lastVectorOnCircle;
    private Vector2 _lastStraghtVector;
    private bool _isGoingBackToCircle = true;
    private bool _isRotatingAwayFromObsctacle = true;
    public EnemyShipStateBackToCircle(GetState function) : base(function)
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
        if (_isRotatingAwayFromObsctacle)
        {
            if (_context.shipRaycasts.isHittingWall)
            {
                _context.enemyTran.Rotate(_context.playerTran.forward, Vector2.SignedAngle(_context.enemyTran.up, Vector2.Lerp(_context.enemyTran.up, (_context.circlingMiddlePoint - _context.enemyPos).normalized, Time.deltaTime)));
            }
            else
            {
                _isRotatingAwayFromObsctacle = false;
                _isGoingBackToCircle = true;
                // if (Physics2D.Raycast(_context.enemyPos, _lastVectorOnCircle.normalized * 1f))
            }
        }
        else
        {
            if (_isGoingBackToCircle)
            {
                _context.enemyPos += (Vector2)_context.enemyTran.up * _context.stats.Speed * Time.deltaTime;
                if(Vector2.Distance(_context.enemyPos,_context.circlingMiddlePoint)>=_context.stats.StartCirclingDistance)
                {
                    _context.agent.enabled = true;
                    ChangeState(EnemyShipStateCircling.StateType);
                }
                if (_context.shipRaycasts.isHittingWall)
                {
                    _isRotatingAwayFromObsctacle = true;
                    _isGoingBackToCircle = false;
                }
            }
            else
            {
                if (Physics2D.Raycast(_context.enemyPos, _lastStraghtVector.normalized))
                {

                }
                else
                {
                    _isGoingBackToCircle = true;
                }
            }

        }
        if(Vector2.Distance(_context.playeryPos,_context.circlingMiddlePoint)>_context.stats.StartCirclingDistance)
        {
            ChangeState(EnemyShipStateChase.StateType);
        }

    }
    public override void SetUpState(EnemyContext context)
    {
        base.SetUpState(context);
        _context = (EnemyShipContext)context;
        _lastVectorOnCircle = _context.enemyTran.up;
        _lastStraghtVector = _lastVectorOnCircle;
        _isRotatingAwayFromObsctacle = true;
        _isGoingBackToCircle = false;
        float angle = Vector2.SignedAngle(_context.enemyTran.up, (_context.circlingMiddlePoint - _context.enemyPos).normalized);
        //_context.enemyTran.Rotate(Vector3.forward, angle);
    }

    public override void InterruptState()
    {
     
    }
}