using System;
using UnityEngine;

public class FollowToTarget : MonoBehaviour
{
    [SerializeField] private bool _isLateUpdate = false;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _activeVec= Vector3.one;

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
        Vector3 targetPosition = _target.position + _offset;
        targetPosition.x = Mathf.Lerp(transform.position.x, targetPosition.x, _activeVec.x);
        targetPosition.y = Mathf.Lerp(transform.position.y, targetPosition.y, _activeVec.y);
        targetPosition.z = Mathf.Lerp(transform.position.z, targetPosition.z, _activeVec.z);
        
        transform.position = targetPosition;
    }
}
