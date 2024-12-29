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
            _laserPointers[i].material.DOColor(_endColor, _emissionColorID, duration).OnComplete(() =>
            {
                Debug.Log("End");
                _laserPointers[i].material.SetColor(_emissionColorID,_laserPointerColor);
                _laserPointers[i].gameObject.SetActive(false);
            });

        }
    }
}
