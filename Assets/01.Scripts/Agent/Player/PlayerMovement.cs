using Crogen.PowerfulInput;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MovementController
{
    [SerializeField] private Transform _attackPointTrm;
    [SerializeField] private Vector3 _attackPointOffset;
    [SerializeField] private Transform _baseTrm;
    [SerializeField] private LayerMask _whatIsGround;

    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _rotateSpeed = 100f;
    
    private InputReader _inputReader;
    
    private Player _player;
    private Rigidbody _rigidbodyCompo;
    public Vector3 lookDirection;
    
    private void Awake()
    {
        _inputReader = GameManager.Instance.InputReader;
        _rigidbodyCompo = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        //Base 회전
        if (lookDirection != Vector3.zero)
        {
            lookDirection.y = 0;
            _baseTrm.rotation = Quaternion.Lerp(this._baseTrm.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * _rotateSpeed);
        }

        HandleLookDirection(_inputReader.MousePosition);
    }

    private void HandleLookDirection(Vector2 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, _whatIsGround))
        {
            lookDirection = hit.point - transform.position;
            _attackPointTrm.position = hit.point + _attackPointOffset;
        }
    }

    public override void StopImmediately()
    {
    }
    
    public override void SetMovement(Vector3 movement, bool isRotation = false)
    {
        if(movement.sqrMagnitude < 0.1f) return;
        transform.DORotateQuaternion(Quaternion.LookRotation(-movement), 0.85f);
        _rigidbodyCompo.velocity = -movement * _moveSpeed;
    }
}