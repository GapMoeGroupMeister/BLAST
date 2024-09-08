using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Nuclear : MonoBehaviour
{
    public int nuclearDamage = 100;
    [SerializeField] private float _waitDelay = 5f;
    private DamageCaster _damageCaster;
    private ParticleSystem _nuclearVFX;
    private DecalProjector _rangeMark;
    private Material _decalMaterial;
    private int _waveSpeedHash;
    private void Awake()
    {
        _waveSpeedHash = Shader.PropertyToID("_Speed");
        _nuclearVFX = GetComponentInChildren<ParticleSystem>();
        _damageCaster = GetComponent<DamageCaster>();
        _rangeMark = transform.Find("TargetMark").GetComponent<DecalProjector>();
        _decalMaterial = _rangeMark.material;
        _rangeMark.enabled = false;
    }

    public void SetPos(Vector3 position)
    {
        transform.position  = position;
    }
    [ContextMenu("DebugExplode")]    
    public void ExplodeNuclear()
    {
        StartCoroutine(ExplodeCoroutine());
    }

    private IEnumerator ExplodeCoroutine()
    {
        _rangeMark.enabled = true;
        _decalMaterial.SetFloat(_waveSpeedHash, 0f);
        float currentTime = 0;
        while (currentTime < _waitDelay)
        {
            currentTime += Time.deltaTime;

            _decalMaterial.SetFloat(_waveSpeedHash, Mathf.Lerp(1f, 5f, currentTime / _waitDelay));
            yield return null;
        }
        _rangeMark.enabled = false;
        _damageCaster.CastDamage(nuclearDamage);
        ZoomController.Instance.ForceZoomOut(60f, 0.5f, 4f);
        _nuclearVFX.Play();
        
    }


}
