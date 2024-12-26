using UnityEngine;
using Crogen.CrogenPooling;

public abstract class FieldObject : MonoBehaviour, IPoolingObject
{
    [SerializeField] private bool _isDestroy;
    public Health HealthCompo { get; private set; }
    public string OriginPoolType { get; set; }
    GameObject IPoolingObject.gameObject { get; set; }

    //[SerializeField] protected PoolType _destroyParticlePoolType;

    private void Awake()
    {
        HealthCompo = GetComponent<Health>();
        _isDestroy = false;
    }

    private void Start()
    {
    }

    protected void HandleDestroy()
    {
        if (_isDestroy) return;
        _isDestroy = true;
        DestroyEvent();
        // EffectObject effectObject = gameObject.Pop(_destroyParticlePoolType, null, Vector3.zero, Quaternion.identity) as EffectObject;
        // effectObject.Play();
        this.Push();
    }

    protected abstract void DestroyEvent();

    public void OnPop()
    {
    }

    public void OnPush()
    {
        _isDestroy = false;
    }
}