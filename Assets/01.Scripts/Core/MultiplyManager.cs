using UnityEngine;

public class MultiplyManager : MonoSingleton<MultiplyManager>
{
    [SerializeField] private UIInputReader _inputReader;
    
    private void Awake()
    {
        _inputReader.OnMultiplyEvent += HandleMultiply;
    }

    private void HandleMultiply(int value)
    {
        TimeManager.Instance.SetDefaultTimeScale(value);
        TimeManager.Instance.PlayTime();
        Debug.Log($"Time Scale: {value}");
    }
}