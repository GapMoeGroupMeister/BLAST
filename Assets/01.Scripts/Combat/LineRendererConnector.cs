using System;
using UnityEngine;

public class LineRendererConnector : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform[] _points;

    private void Awake()
    {
        _lineRenderer.useWorldSpace = true;
    }

    private void LateUpdate()
    {
        _lineRenderer.positionCount = _points.Length;

        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            _lineRenderer.SetPosition(i, _points[i].position);
        }
    }
}
