using UnityEngine;

public class FollowToTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    private void FixedUpdate()
    {
        transform.position = _target.position + _offset;
    }
}
