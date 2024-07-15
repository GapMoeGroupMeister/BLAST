using System;
using DG.Tweening;
using UnityEngine;

public class RedZone : MonoBehaviour
{
    public SphereCollider AreaCollider { get; private set; }

    private void Awake()
    {
        AreaCollider = GetComponent<SphereCollider>();
    }
    
    public void RedZoneSet(Vector3 position, Vector3 scale, Ease ease,TweenCallback onComplete = null, float openDuration = 0.5f)
    {
        transform.position = position;
        transform.DOScale(scale, openDuration).SetEase(ease)
            .OnComplete(onComplete);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(100);
        }
    }
}