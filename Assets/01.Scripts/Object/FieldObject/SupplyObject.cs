using UnityEngine;

public class SupplyObject : FieldObject
{
    [SerializeField] private SupplyUI _supplyUIPf;
    private Transform _canvas;
    private SupplyUI _uiInstance;

    private void Awake()
    {
        _canvas = GameObject.Find("Canvas").transform;
    }

    private void OnEnable()
    {
        _uiInstance = Instantiate(_supplyUIPf, _canvas);
        _uiInstance.Init(transform);
    }

    protected override void DestroyEvent()
    {
        Destroy(_uiInstance);
    }
}