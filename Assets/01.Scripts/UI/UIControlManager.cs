using System;
using UnityEngine;

public class UIControlManager : MonoBehaviour
{
    public Vector3 mousePosition;

    [SerializeField] private float _detectDistance;
    [SerializeField] private LayerMask _uiLayer;

    private IClickable _currentObject = null;
    private bool _isTargeted;
    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, _detectDistance, _uiLayer);
        if (hit.collider == null) return;

        if (hit.transform.TryGetComponent(out IClickable clickTarget))
        {
            _currentObject = clickTarget;
        }

        
    }
    
}