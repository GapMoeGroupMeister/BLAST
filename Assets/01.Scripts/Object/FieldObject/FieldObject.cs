using ObjectPooling;
using UnityEngine;

public abstract class FieldObject : PoolableMono
{
    [SerializeField] private bool _isDestroy;
    public Health HealthCompo { get; private set; }
    [SerializeField] protected PoolingType _destroyParticlePoolType;

    private void Awake()
    {
        HealthCompo = GetComponent<Health>();
        _isDestroy = false;
    }

    private void Start()
    {
    }


    public override void ResetItem()
    {
        _isDestroy = false;
        
    }

    protected void HandleDestroy()
    {
        if (_isDestroy) return;
        _isDestroy = true;
        DestroyEvent();
        EffectObject effectObject = PoolingManager.Instance.Pop(_destroyParticlePoolType) as EffectObject;
        effectObject.Play();
        PoolingManager.Instance.Push(this);
    }

    protected abstract void DestroyEvent();





}