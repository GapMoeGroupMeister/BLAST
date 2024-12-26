using DG.Tweening;
using Crogen.CrogenPooling;
using System.Collections;
using UnityEngine;
using System;

public class TrailEffect : MonoBehaviour, IPoolingObject
{
    private TrailRenderer _trailRenderer;
    private Vector3 _targetPos; 
    private Material _material;
    private readonly int _alphaHash = Shader.PropertyToID("_Alpha");

    public string OriginPoolType { get; set; }
    GameObject IPoolingObject.gameObject { get; set ; }

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _material = _trailRenderer.material;
        _material.SetFloat(_alphaHash, 1);
        _trailRenderer.enabled = false;
    }

    public void SetTrail(Vector3 startPos, Vector3 endPos, float duration, Action EndEvent = null)
    {
        _trailRenderer.enabled = true;
        transform.position = startPos;
        _targetPos = endPos;
        StartCoroutine(TrailCoroutine(duration, EndEvent));
    }

    private IEnumerator TrailCoroutine(float duration, Action EndEvent = null)
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
        DOTween.To(() => _material.GetFloat(_alphaHash), v => _material.SetFloat(_alphaHash, v), 0, 1f);
        yield return new WaitForSeconds(duration);
        EndEvent?.Invoke();
    }

    public void OnPop()
    {
    }

    public void OnPush()
    {
        _trailRenderer.Clear();
        _trailRenderer.enabled = false;
        _material.SetFloat(_alphaHash, 1);
    }
}
