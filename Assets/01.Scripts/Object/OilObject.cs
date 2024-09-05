using System.Collections;
using Crogen.ObjectPooling;
using EffectSystem;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OilObject : MonoBehaviour, IPoolingObject, IEffectable
{
    public PoolType OriginPoolType { get; set; }
    public GameObject gameObject { get; set; }

    private DecalProjector _decalCompo;
    private Material _decalMaterial;
    [SerializeField] private float _detectRange = 4f;
    private int setOilAmount = 0;
    public int leftOilAmount = 0;
    [SerializeField] private LayerMask _targetLayer;
    private bool _isFire;
    private Collider[] hits;
    private ParticleSystem _fireVFX;

    private int _dissolveHash;
    private int _randomSeedHash;

    [SerializeField] private float _burnAroundDuration = 0.5f;
    private float _currentTime = 0;
    
    private void Awake()
    {
        hits = new Collider[3];
        _dissolveHash = Shader.PropertyToID("_DissolveHeight");
        _randomSeedHash = Shader.PropertyToID("_RandomSeed");
        _decalCompo = GetComponentInChildren<DecalProjector>();
        _decalMaterial = _decalCompo.material;
        _fireVFX = transform.Find("FireVFX").GetComponent<ParticleSystem>();

    }

    public void SetOil(int amount)
    {
        _decalMaterial.SetFloat(_dissolveHash, 0.7f);
        leftOilAmount = amount;
        setOilAmount = amount;
        _isFire = false;
    }

    private void FixedUpdate()
    {
        if (_isFire)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _burnAroundDuration)
            {
                _currentTime = 0;
                BurnAround();
                
            }

        }
        
    }


    private void BurnAround()
    {
        int amount =  Physics.OverlapSphereNonAlloc(transform.position, _detectRange, hits, _targetLayer);
        if (amount == 0) return;
        leftOilAmount--;
        _decalMaterial.SetFloat(_dissolveHash, Mathf.Lerp(0f, 0.7f, leftOilAmount/(float)setOilAmount));
        for (int i = 0; i < amount; i++)
        {
            if (hits[i].transform.TryGetComponent(out IEffectable effectTarget))
            {
                effectTarget.ApplyEffect(EffectStateTypeEnum.Burn, 1f,1);
            }
        }

        if (leftOilAmount <= 0)
        {
            _isFire = false;
            StartCoroutine(SetOffFireCoroutine());
        }
    }

    private IEnumerator SetOffFireCoroutine()
    {
        _fireVFX.Stop();
        yield return new WaitForSeconds(2f);
        this.Push();
    }

    public void OnPop()
    {
        _isFire = false;
        _decalMaterial.SetFloat(_randomSeedHash, Random.Range(-10f, 10f));
    }

    public void OnPush()
    {
        
        
        
    }

    [ContextMenu("DebugSEtOil")]
    private void DEbugSetOil()
    {
        SetOil(100);
    }
    
    [ContextMenu("DebugFire")]
    private void FireDick()
    {
        ApplyEffect(EffectStateTypeEnum.Burn, 1, 1);
    } 
    
    
    public void ApplyEffect(EffectStateTypeEnum type, float duration, int level)
    {
        if(_isFire || leftOilAmount <= 0) return;
        if (type == EffectStateTypeEnum.Burn)
        {
            _isFire = true;
            _fireVFX.Play();
        }
    }

   
}
