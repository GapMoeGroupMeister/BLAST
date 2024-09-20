using System;
using Unity.VisualScripting;
using UnityEngine;

public class UIControlManager : MonoSingleton<UIControlManager>
{
    public Vector3 mousePosition;

    [SerializeField] private float _detectDistance;
    [SerializeField] private LayerMask _uiLayer;

    private IClickable _prevObject;
    private IClickable _currentObject = null;
    public int overUIAmount; // 오브젝트 UI보다 선행적으로 인식되어야하는 active된 Canvas UI의 개수
    private bool _isTargeted;
    private Ray _ray;

    private void Awake()
    {
        overUIAmount = 0;
    }
    
    private void Update()
    {
        if(overUIAmount > 0) return;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_ray, out RaycastHit hit, _detectDistance, _uiLayer);

        if (hit.collider == null)
        {
            if (_currentObject != null)
            {
                _currentObject.Exit();
                _prevObject = _currentObject;
            }
            _isTargeted = false;
            _currentObject = null;
            return;
        }

       
        
        if (hit.transform.TryGetComponent(out IClickable clickTarget))
        {
            
            if(_currentObject == clickTarget) clickTarget.Enter();
            _prevObject = _currentObject;
            if (_currentObject != _prevObject)
            {
                _currentObject.Exit();
                _prevObject = _currentObject;
                return;
            
            }
            _currentObject = clickTarget;
            
        }

        if (_currentObject == null) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            _currentObject.Click();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _currentObject.Release();
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_ray);
    }
}