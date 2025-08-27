using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] bool _debug;
    [SerializeField] PlayerController _player;
    [SerializeField] PlayerMovement2D _movement;
    [SerializeField] bool _useCommands;
    [SerializeField] PlayerInputStack _inputStack;
    [SerializeField] GameEventVoidSO _pauseEvent;
    [SerializeField] GunsComponent _playerGuns;
    private Vector2 _direction;
    private float _angleRoot;
    private float _move;
    private bool _canMove=true;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.IsAlive)
        {

            if (!PauseSettings.IsGamePaused)
            {
                if (!_canMove) return;
                if (_move != 0)
                {
                    if (_move > 0) _movement.IncreaseSpeed();
                    else _movement.DecreaseSpeed();
                }

            }
        }
    }
    private void FixedUpdate()
    {
        if (!PauseSettings.IsGamePaused)
        {
            if (!_canMove) return;
            _player.CurrentPlayerState.Move(_direction);
            if (_angleRoot != 0)
            {
                if (_angleRoot > 0) _movement.DecreaseAngle();
                else _movement.IncreaseAngle();
            }
        }
    }
    void OnMousePos(InputValue value)
    {
        HelperClass.SetMousePos(value.Get<Vector2>());
        _playerGuns.LookAtMouse();
    }
    public void ChangeCanMove()
    {
        _canMove = !_canMove;
    }
    private void OnMove(InputValue value)
    {
        _direction = value.Get<Vector2>();
        if (_debug) Logger.Log(_direction);
    }
    private void OnInteract(InputValue value)
    {
        if (!_canMove) return;
        _player.Interact();
    }
    void OnJump(InputValue value)
    {
        if (PauseSettings.IsGamePaused) return;
        if (_useCommands) _inputStack.CurrentCommand= new JumpInputCommand(_player.CurrentPlayerState);
        else _player.CurrentPlayerState.Jump();

    }
    void OnHorizontal(InputValue value)
    {

        _angleRoot = value.Get<float>();
        Logger.Log(_angleRoot);
    }
    void OnVertical(InputValue value)
    {
        _move = value.Get<float>();

    }
    void OnSetAmount(InputValue value)
    {
        if (value.Get<float>() > 0) _player.IncreasePillagingCrew();
        else _player.DecreasePillagingCrew();
    }
    private void OnPause()
    {
        _pauseEvent.Raise();
    }
    private void OnAttack(InputValue value)
    {
        if (PauseSettings.IsGamePaused) return;
        if (_useCommands)
        {
            _inputStack.CurrentCommand = new AttackInputCommand(_player.CurrentPlayerState);
            if (_direction.y > 0) _inputStack.CurrentCommand = new AttackInputCommand(_player.CurrentPlayerState, PlayerCombat.AttackModifiers.UP_ARROW);
            if (_direction.y < 0) _inputStack.CurrentCommand = new AttackInputCommand(_player.CurrentPlayerState, PlayerCombat.AttackModifiers.DOWN_ARROW);
        }
        else
        {
            
            if(_direction.y==0) _player.CurrentPlayerState.Attack();
            else if (_direction.y > 0) _player.CurrentPlayerState.Attack(PlayerCombat.AttackModifiers.UP_ARROW);
            else if (_direction.y < 0) _player.CurrentPlayerState.Attack(PlayerCombat.AttackModifiers.DOWN_ARROW);
        }
    }
}
