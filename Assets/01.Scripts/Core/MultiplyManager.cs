using UnityEngine;

public class MultiplyManager : MonoSingleton<MultiplyManager>
{
    [SerializeField] private UIInputReader _inputReader;
    
    protected override void Awake()
    {
        base.Awake();
        _inputReader.OnMultiplyEvent += HandleMultiply;
    }

    private void HandleMultiply(int value)
    {
        TimeManager.Instance.SetDefaultTimeScale(value);
        TimeManager.Instance.PlayTime();
        Debug.Log($"Time Scale: {value}");
    }
}