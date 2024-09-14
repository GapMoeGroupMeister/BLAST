using UnityEngine;

public class FireBullet : Bullet
{
    private MeshRenderer _visualMesh;
    private Material _fireMaterial;
    private int _visualOffsetHash;
    [SerializeField] private float _castTerm;
    private float _currentTime;

    protected override void Awake()
    {
        _visualMesh = transform.Find("Visual").GetComponent<MeshRenderer>();
        _fireMaterial = _visualMesh.material;
        _visualOffsetHash = Shader.PropertyToID("_VisualOffset");
    }

    public override void OnPop()
    {
        base.OnPop();
        ApplyOffset();
    }

    protected override void FixedUpdate()
    {
        _currentTime += Time.fixedDeltaTime;
        if (_currentTime >= _castTerm)
        {
            _damageCaster.CastDamage(_damage);
        }
    }

    private void ApplyOffset()
    {
        _fireMaterial.SetFloat(_visualOffsetHash, Random.Range(0, 40));
    }
}
