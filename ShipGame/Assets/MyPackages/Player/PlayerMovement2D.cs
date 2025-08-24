using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public int FlipSide => _flipSide;
    public bool IsPlayerFalling { get => _rb.linearVelocity.y < 0; }
    public Rigidbody2D PlayerRB => _rb;
    [Header("Common")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Transform _mainBody;
    [SerializeField] PlayerController _player;
    [SerializeField] PlayerRaycasts _raycasts;
    [SerializeField] float _speed;
    [Header("Jump")]
    [SerializeField] Ringhandle _jumpHandle;
    [SerializeField] SlopeDetection _slopeDetection;
    [SerializeField] float _jumpStrength;

    [Header("Speeds")]
    [SerializeField] float _acceleration;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _minSpeed;
    [SerializeField] float _walkSpeed = 2f;
    [SerializeField] float _runSpeed = 5f;
    [SerializeField] float _sprintSpeed = 10f;
    [SerializeField, Header("Rotations")] float _angularRotationSpeed;
    
    private int _flipSide = 1;
    private GlobalEnums.HorizontalDirections _newPlayerDirection;
    private GlobalEnums.HorizontalDirections _oldPlayerDirection;
    private float _previousDirection;

    private bool _canMove = true;
    private float _moveSpeed;
    private float _targetRotAngle;
    private Vector2 _dirVector=Vector2.up;
    public void MoveInAir(Vector2 direction,bool canRotate)
    {
        if (direction.x != 0)
        {
            _oldPlayerDirection = _newPlayerDirection;
            _newPlayerDirection = (GlobalEnums.HorizontalDirections)direction.x;
            _rb.MovePosition(_rb.position + new Vector2(_mainBody.right.x * _flipSide * _speed * Time.deltaTime * 0.6f, _rb.position.y));
            _previousDirection = direction.x;
        }
        else
        {
            if (_previousDirection != 0)
            {
                _rb.linearVelocity = new Vector2(0, 0);
                _rb.MovePosition(_rb.position + new Vector2(_mainBody.right.x * _flipSide * _speed * Time.deltaTime, 0));
                //StopPlayerOnXAxis();
            }

        }
    }
    public void Move(Vector2 direction,Vector2 groundRayHitPoint, Vector2 groundNormal)
    {
        if (!_slopeDetection.CanWalkOnSlope()) return;
        if (direction.x != 0)
        {
            _oldPlayerDirection = _newPlayerDirection;
            _newPlayerDirection = (GlobalEnums.HorizontalDirections)direction.x;
            _rb.MovePosition(_rb.position + new Vector2(_mainBody.right.x * _flipSide * _speed * Time.deltaTime, -(_rb.position.y- groundRayHitPoint.y)));
            if (direction.x > 0)
            {
                _flipSide = 1;
                _player.MainBody.transform.localScale = new Vector3(_flipSide, _player.MainBody.transform.localScale.y, _player.MainBody.transform.localScale.z);
            }
            if (direction.x < 0)
            {
                _flipSide = -1;
                _player.MainBody.transform.localScale = new Vector3(_flipSide, _player.MainBody.transform.localScale.y, _player.MainBody.transform.localScale.z);
            }
            _previousDirection = direction.x;
        }
        else
        {
            if (_previousDirection != 0)
            {
                _rb.linearVelocity = new Vector2(0, 0);
                _rb.MovePosition(_rb.position + new Vector2(_mainBody.right.x * _flipSide * _speed * Time.deltaTime, 0));
            }

        }

    }
    public void Stop()
    {
        _moveSpeed = 0;
    }
    private void FixedUpdate()
    {
        if (_raycasts.isHittingWall) return;
        _rb.MovePosition(_rb.transform.position + _rb.transform.up * _moveSpeed * Time.deltaTime);
    }
    public void IncreaseSpeed()
    {
        _moveSpeed += Time.deltaTime * _acceleration;
        if (_moveSpeed > _maxSpeed) _moveSpeed = _maxSpeed;
    }
    public void DecreaseSpeed()
    {

        _moveSpeed -= Time.deltaTime * _acceleration;
        if (_moveSpeed < _minSpeed) _moveSpeed = _minSpeed;
    }
    public void IncreaseAngle()
    {
        _targetRotAngle += _angularRotationSpeed * Time.deltaTime;
        if (_targetRotAngle >= 360) _targetRotAngle = 0;
        _rb.SetRotation(_targetRotAngle);
    }
    public void DecreaseAngle()
    {
        _targetRotAngle -= _angularRotationSpeed * Time.deltaTime; ;
        if (_targetRotAngle <= -360) _targetRotAngle = 0;
        _rb.SetRotation(_targetRotAngle);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_rb.transform.position, _rb.transform.position + (Vector3)_dirVector * 2f);
    }
}
