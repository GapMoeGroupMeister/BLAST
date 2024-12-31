using DG.Tweening;
using UnityEngine;

public class Boss01 : Boss
{
    [SerializeField]
    private Renderer[] _laserPointers;
    [ColorUsage(true, true)]
    [SerializeField]
    private Color _laserPointerColor, _endColor;
    private readonly int _emissionColorID = Shader.PropertyToID("_EmissionColor");
    [SerializeField]
    private LaserAligner _laserAligner;

    protected override void Awake()
    {
        base.Awake();
        _laserAligner.transform.SetParent(null);
    }

    public void ShootBulletEffect(float duration)
    {
        for(int i = 0; i < _laserPointers.Length; i++)
        {
            var pointer = _laserPointers[i];
            pointer.gameObject.SetActive(true);
            Material material = pointer.material;
            material.SetColor(_emissionColorID, _laserPointerColor);
            material.DOColor(_endColor, _emissionColorID, duration).OnComplete(() => 
            {
                material.SetColor(_emissionColorID, _laserPointerColor);
                material.DOColor(_endColor, _emissionColorID, 0.15f);
            });
            DOVirtual.DelayedCall(duration + 0.15f, () => pointer.gameObject.SetActive(false));
        }
    }

    public void EnableLaser()
    {
        //todo : 레이저 갯수 Level 기준으로 다시 변경하기
        //int count = Mathf.CeilToInt(Level * 3) + 1;
        int count = 3;
        _laserAligner.SetActiveLaser(true, count);
    }
}
