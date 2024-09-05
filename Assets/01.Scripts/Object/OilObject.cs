using EffectSystem;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class OilObject : MonoBehaviour, IPoolingObject, IEffectable
{
    public PoolType OriginPoolType { get; set; }
    public GameObject gameObject { get; set; }

    private DecalProjector _decalCompo;
    private Material _decalMaterial;
    [SerializeField] private float _detectRange = 4f;   
    public int leftOilAmount = 0;
    [SerializeField] private LayerMask _targetLayer;
    private bool _isFire;
    private Collider[] hits;
    private ParticleSystem _fireVFX;

    private int _dissolveHash;
    private int _randomSeedHash;
    
    private void Awake()
    {
        hits = new Collider[3];
        _dissolveHash = Shader.PropertyToID("DissolveHeight");
        _randomSeedHash = Shader.PropertyToID("RandomSeed");
        _decalCompo = GetComponentInChildren<DecalProjector>();
        _decalMaterial = _decalCompo.material;

    }

    public void SetOil(int amount)
    {
        _decalMaterial.SetFloat(_dissolveHash, amount);
        leftOilAmount = amount;
        _isFire = false;
    }

    private void FixedUpdate()
    {
        if(_isFire)
            BurnAround();
        
    }


    private void BurnAround()
    {
        int amount =  Physics.OverlapSphereNonAlloc(transform.position, _detectRange, hits, _targetLayer);
        if (amount == 0) return;
        
        for (int i = 0; i < amount; i++)
        {
            if (hits[i].transform.TryGetComponent(out IEffectable effectTarget))
            {
                effectTarget.ApplyEffect(EffectStateTypeEnum.Burn, 1f,1);
            }
        }
    }

    public void OnPop()
    {
        _isFire = false;
        _decalMaterial.SetFloat(_randomSeedHash, Random.Range(-10f, 10f));
    }

    public void OnPush()
    {
        
    }

    public void ApplyEffect(EffectStateTypeEnum type, float duration, int level)
    {
        if(_isFire) return;
        if (type == EffectStateTypeEnum.Burn)
        {
            _isFire = true;
            _fireVFX.Play();
        }
    }

   
}
