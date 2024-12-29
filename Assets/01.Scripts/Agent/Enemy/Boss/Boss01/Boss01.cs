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

    public void ShootBulletEffect(float duration)
    {
        for(int i = 0; i < _laserPointers.Length; i++)
        {
            _laserPointers[i].gameObject.SetActive(true);
            Material material = _laserPointers[i].material;
            material.SetColor(_emissionColorID, _laserPointerColor);
            material.DOColor(_endColor, _emissionColorID, duration).OnComplete(() =>
            {
                _laserPointers[i].gameObject.SetActive(false);
            });

        }
    }
}
