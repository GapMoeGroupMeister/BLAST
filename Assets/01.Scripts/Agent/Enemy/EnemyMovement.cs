using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyMovement : MovementController
{
    public Vector3 Velocity { get; }

    public bool IsGround { get; }

    private Enemy _enemy;
    private NavMeshAgent _navAgent;
    public NavMeshAgent NavAgent => _navAgent;
    private Rigidbody _rigidbodyCompo;

    [SerializeField]
    private float _knockbackThreshold;
    [SerializeField]
    private float _maxKnockbackTime;
    [SerializeField]
    private float _physicsDelayTime;

    private float _currentKnockbackTime;
    private bool _isKnockback;

    public void Initialize(Agent agent)
    {
        _enemy = agent as Enemy;
        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.speed = _enemy.Stat.GetValue(StatEnum.Speed);

        _rigidbodyCompo = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
            ForceMove(Vector3.zero);
    }

    public void DisableNavAgent()
    {
        _navAgent.enabled = false;
    }

    public void SetDestination(Vector3 destination)
    {
        if (!_navAgent.enabled) return;
        _navAgent.isStopped = false;
        _navAgent.SetDestination(destination);
    }

    public void GetKnockback(Vector3 force)
    {
        StartCoroutine(ApplyKnockback(force));
    }

    private IEnumerator ApplyKnockback(Vector3 force)
    {
        Vector3 destination = _navAgent.destination;

        _navAgent.enabled = false;
        _rigidbodyCompo.useGravity = true;
        _rigidbodyCompo.isKinematic = false;
        _rigidbodyCompo.AddForce(force, ForceMode.Impulse);
        _knockbackThreshold = Time.time;
        if (_isKnockback)
        {
            yield break;
        }

        _isKnockback = true;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        yield return new WaitUntil(() => _rigidbodyCompo.velocity.magnitude < _knockbackThreshold || Time.time > _currentKnockbackTime + _maxKnockbackTime);

        _rigidbodyCompo.velocity = Vector3.zero;
        _rigidbodyCompo.angularVelocity = Vector3.zero;
        _rigidbodyCompo.useGravity = false;
        _rigidbodyCompo.isKinematic = true;

        _navAgent.Warp(transform.position);
        _navAgent.enabled = true;
        _isKnockback = false;
    }

    public override void StopImmediately()
    {
        if (!_navAgent.enabled) return;
        _navAgent.velocity = Vector3.zero;
        _navAgent.isStopped = true;
    }

    public void ForceMove(Vector3 position)
    {
        StartCoroutine(ForceMoveCoroutine(position));
    }

    private IEnumerator ForceMoveCoroutine(Vector3 position)
    {
        _navAgent.enabled = false;
        transform.position = position;
        yield return null;
        _navAgent.Warp(transform.position);
        _navAgent.enabled = true;
    }

    public override void SetMovement(Vector3 movement, bool isRotation = false)
    {
    }
}
