using DG.Tweening;
using Crogen.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour, IPoolingObject
{
    private TrailRenderer _trailRenderer;
    private Vector3 _targetPos; 
    private Material _material;
    private readonly int _alphaHash = Shader.PropertyToID("_Alpha");

    public PoolType OriginPoolType { get; set; }
    GameObject IPoolingObject.gameObject { get; set ; }

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _material = _trailRenderer.material;
        _material.SetFloat(_alphaHash, 1);
        _trailRenderer.enabled = false;
    }

    public void SetTrail(Vector3 startPos, Vector3 endPos, float duration)
    {
        _trailRenderer.enabled = true;
        transform.position = startPos;
        _targetPos = endPos;
        StartCoroutine(TrailCoroutine(duration));
    }

    private IEnumerator TrailCoroutine(float duration)
    {
        Vector3 startPos = transform.position;
        float percent = 0;
        yield return null;
        while (percent < 1)
        {
            percent += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(startPos, _targetPos, percent);
            yield return null;
        }

        DOTween.To(() => _material.GetFloat(_alphaHash), v => _material.SetFloat(_alphaHash, v), 0, 1f).
            OnComplete(() => { 
                this.Push(); 
                _trailRenderer.Clear(); 
            });
    }

    public void OnPop()
    {
    }

    public void OnPush()
    {
        _trailRenderer.enabled = false;
        _material.SetFloat(_alphaHash, 1);
    }
}
