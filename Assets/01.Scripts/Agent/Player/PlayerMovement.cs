using Crogen.PowerfulInput;
using UnityEngine;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine.Events;

public class PlayerMovement : MovementController
{
    public event Action<float> OnDistanceTravelledEvent;
    public event Action<float> OnDashCoolTimePercentEvent;
    public event Action<Vector3> OnDashDirectionEvent;
    public UnityEvent OnDashEvent;
    [SerializeField] private Transform _attackPointTrm;
    [SerializeField] private Vector3 _attackPointOffset;
    [SerializeField] private Transform _baseTrm;
    [SerializeField] private LayerMask _whatIsGround;

    public float moveSpeed = 2f;
    [SerializeField] private float _rotateSpeed = 100f;
    public float dashPower = 5f;
    public float dashCooltime = 2f;
    private float _curDashCooltime = 0;
    public bool canDash;
    private InputReader _inputReader;
    
    private Player _player;
    private Rigidbody _rigidbodyCompo;
    public Vector3 lookDirection;
    private bool _canMove = true;
    private float _distanceTravelled;

    private void Awake()
    {
        _curDashCooltime = dashCooltime;
        _inputReader = GameManager.Instance.InputReader;
        _rigidbodyCompo = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
        moveSpeed = _player.Stat.GetValue(StatEnum.Speed);
    }

	private void Update()
	{
        _curDashCooltime += Time.deltaTime;
        if (_curDashCooltime > dashCooltime)
            canDash = true;
        else
            canDash = false;
        OnDashCoolTimePercentEvent?.Invoke(_curDashCooltime/dashCooltime);
    }

	private void FixedUpdate()
    {
        //Base 회전
        if (lookDirection != Vector3.zero)
        {
            lookDirection.y = 0;
            if (_canMove == false) return;
            _baseTrm.rotation = Quaternion.Lerp(this._baseTrm.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * _rotateSpeed);
        }

        HandleLookDirection(_inputReader.MousePosition);
    }

    private void HandleLookDirection(Vector2 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, _whatIsGround))
        {
            lookDirection = (hit.point - transform.position).normalized;
            if(_attackPointTrm.gameObject.activeSelf)
                _attackPointTrm.position = hit.point + _attackPointOffset;
        }
    }

    public void SetCanMove(bool value)
    {
        _attackPointTrm.gameObject.SetActive(value);
        _canMove = value;
    }

    public override void StopImmediately()
    {
        _rigidbodyCompo.velocity = Vector3.zero;
    }

    public void OnDash(Vector3 dashDir, float duration, float dashPower, Action EndEvent = null)
	{
        _curDashCooltime = 0;
        OnDashDirectionEvent?.Invoke(dashDir);
        OnDashEvent?.Invoke();
        StartCoroutine(CoroutineOnDash(dashDir, duration, dashPower, EndEvent));
	}

    private IEnumerator CoroutineOnDash(Vector3 dashDir, float duration, float dashPower, Action EndEvent)
	{
        transform.rotation = Quaternion.LookRotation(dashDir);
        _rigidbodyCompo.AddForce(dashDir * dashPower * duration * _rigidbodyCompo.mass, ForceMode.Impulse);
        yield return new WaitForSeconds(duration-(duration/6f));
        EndEvent?.Invoke();
    }

    private Vector3 _startPos;
    private Vector3 _endPos;
    public override void SetMovement(Vector3 movement, bool isRotation = false)
    {
        if (_canMove == false) return;
        _startPos = transform.position;
        if (movement.sqrMagnitude < 0.1f) return;

		movement = Quaternion.Euler(0, -45, 0) * movement;
        transform.DORotateQuaternion(Quaternion.LookRotation(-movement), 0.85f);
        _rigidbodyCompo.velocity = -movement * moveSpeed;

        _distanceTravelled += (_startPos - _endPos).magnitude;
        OnDistanceTravelledEvent?.Invoke(_distanceTravelled);
        _endPos = _startPos;
    }
}