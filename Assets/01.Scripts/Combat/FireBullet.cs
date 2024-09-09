using UnityEngine;

public class FireBullet : Bullet
{
    private MeshRenderer _visualMesh;
    private Material _fireMaterial;
    private int _visualOffsetHash;

    protected override void Awake()
    {
        base.Awake();
        _visualMesh = transform.Find("Visual").GetComponent<MeshRenderer>();
        _fireMaterial = _visualMesh.material;
        _visualOffsetHash = Shader.PropertyToID("_VisualOffset");
    }

    public override void OnPop()
    {
        base.OnPop();
        ApplyOffset();
    }

    private void ApplyOffset()
    {
        _fireMaterial.SetFloat(_visualOffsetHash, Random.Range(0, 40));
    }
}
