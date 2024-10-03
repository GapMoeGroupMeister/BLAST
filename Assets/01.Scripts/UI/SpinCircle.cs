using UnityEngine;

public class SpinCircle : MonoBehaviour
{
    [SerializeField] private float _speed;
    private RectTransform _circleRect;

    private void Awake()
    {
        _circleRect = GetComponent<RectTransform>();
    }
    
    private void Update()
    {
        _circleRect.Rotate(0, 0, _speed * Time.deltaTime);
    }
}
