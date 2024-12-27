using Crogen.CrogenPooling;
using DG.Tweening;
using UnityEngine;

public class MissileMine : WeaponEffect, IPoolingObject
{
    public string OriginPoolType { get; set; }
    GameObject IPoolingObject.gameObject { get; set; }

    [SerializeField] private EffectPoolType _destroyEffectPoolType;
    [SerializeField] private float _endRange = 50;
    [SerializeField] private float _moveDuration = 2f;
    [SerializeField] private float _lifeTime = 3f;
    [SerializeField] private float _curLifeTime = 0f;
    [SerializeField] private float _maxHeight = 60f;
    
    public void OnPop()
    {
        
    }

    public void OnPush()
    {
        gameObject.Pop(_destroyEffectPoolType, transform.position, Quaternion.identity);
        CameraShakeController.Instance.ShakeCam(3, 0.08f);
        _curLifeTime = 0;
    }

    private Vector3 _lastPosition;
    
    private void Update()
    {
        transform.forward = (transform.position - _lastPosition).normalized;
        _lastPosition = transform.position;       
    }

    public void SetDirection(Vector3 direction)
    {
        float randRange = UnityEngine.Random.Range(-_endRange, _endRange);
        
        Vector3 finalPosition = direction.normalized * randRange + transform.position;
        
        
        transform.DOJump(finalPosition, _maxHeight, 1, _moveDuration)
            .SetEase(Ease.InSine)
            .OnComplete(this.Push);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _endRange);
    }
}
