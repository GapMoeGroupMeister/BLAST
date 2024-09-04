using Crogen.PowerfulInput;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerMovement : MovementController
{
    public event Action<float> OnDistanceTravelledEvent;

    [SerializeField] private Transform _attackPointTrm;
    [SerializeField] private Vector3 _attackPointOffset;
    [SerializeField] private Transform _baseTrm;
    [SerializeField] private LayerMask _whatIsGround;

    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _rotateSpeed = 100f;
    
    private InputReader _inputReader;
    
    private Player _player;
    private Rigidbody _rigidbodyCompo;
    private Vector3 _lookDirection;

    private float _distanceTravelled;

    private void Awake()
    {
        _inputReader = GameManager.Instance.InputReader;
        _rigidbodyCompo = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        //Base 회전
        if (_lookDirection != Vector3.zero)
        {
            _lookDirection.y = 0;
            _baseTrm.rotation = Quaternion.Lerp(this._baseTrm.rotation, Quaternion.LookRotation(_lookDirection), Time.deltaTime * _rotateSpeed);
        }

        HandleLookDirection(_inputReader.MousePosition);
    }

    private void HandleLookDirection(Vector2 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, _whatIsGround))
        {
            _lookDirection = hit.point - transform.position;
            _attackPointTrm.position = hit.point + _attackPointOffset;
        }
    }

    public override void StopImmediately()
    {
    }

    private Vector3 _startPos;
    private Vector3 _endPos;
    public override void SetMovement(Vector3 movement, bool isRotation = false)
    {
        _startPos = transform.position;
        if (movement.sqrMagnitude < 0.1f) return;

		movement = Quaternion.Euler(0, -45, 0) * movement;
        transform.DORotateQuaternion(Quaternion.LookRotation(-movement), 0.85f);
        _rigidbodyCompo.velocity = -movement * _moveSpeed;

        _distanceTravelled += (_startPos - _endPos).magnitude;
        OnDistanceTravelledEvent?.Invoke(_distanceTravelled);
        _endPos = _startPos;
    }
}