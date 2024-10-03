using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MagneticBlock : MonoBehaviour
{

    public float pullPower = 4f;
    public float detectRange = 10f;
    public float duration = 3f;
    [SerializeField] private LayerMask _enemyLayer;
    private Collider[] _colliders;
    private ParticleSystem _generateVFX;
    private ParticleSystem _magneticVFX;
    private DecalProjector _rangeDecal;
    private bool _isActive;

    private void Awake()
    {
        _generateVFX = transform.Find("GenerateVFX").GetComponent<ParticleSystem>();
        _magneticVFX = transform.Find("VFX").GetComponent<ParticleSystem>();
        _rangeDecal = transform.Find("RangeDecal").GetComponent<DecalProjector>();
        _colliders = new Collider[10];
    }

    [ContextMenu("DebugActive")]
    public void Active(Vector3 pos, float power, float range)
    {

        transform.position = pos;
        _isActive = true;
        _generateVFX.Play();
        _rangeDecal.enabled = true;
        _rangeDecal.size = new Vector3(detectRange, detectRange, _rangeDecal.size.z);
        StartCoroutine(ActiveCoroutine());
    }

    private IEnumerator ActiveCoroutine()
    {
        _magneticVFX.Play();
        yield return new WaitForSeconds(duration);
        _magneticVFX.Stop();
        _isActive = false;
        _rangeDecal.enabled = false;
    }

    private void FixedUpdate()
    {
        if (_isActive)
            Pull();

    }

    private void Pull()
    {
        int amount = Physics.OverlapSphereNonAlloc(transform.position, detectRange, _colliders, _enemyLayer);

        for (int i = 0; i < amount; i++)
        {
            Collider target = _colliders[i];
            if (target.TryGetComponent(out EnemyMovement movement))
            {
                movement.ForceMove((target.transform.position - transform.position) * pullPower);
            }
        }
    }
}
