using System;
using UnityEngine;

public class FollowToTarget : MonoBehaviour
{
    [SerializeField] private bool _isLateUpdate = false;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        if(_isLateUpdate == false)
            FollowTarget();
    }

    private void LateUpdate()
    {
        if (_isLateUpdate == true)
            FollowTarget();
    }

    private void FollowTarget()
    {
        transform.position = _target.position + _offset;
    }
}
