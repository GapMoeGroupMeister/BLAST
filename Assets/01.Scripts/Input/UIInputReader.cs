using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "UIInputReader", menuName = "SO/UI_InputReader")]
public class UIInputReader : ScriptableObject, Controls.IUIActions
{
    public event Action OnEscEvent;
    public event Action<int> OnMultiplyEvent;

    private Controls _controls;
    private int _multiplyValue = 1;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.UI.SetCallbacks(this);
        }
        _controls.UI.Enable();
    }

    private void OnDisable()
    {
        OnEscEvent = null;
        OnMultiplyEvent = null;
        
        _controls.Disable();
    }


    public void OnEsc(InputAction.CallbackContext context)
    {
        OnEscEvent?.Invoke();
    }

    public void OnMultiply(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_multiplyValue >= 3)
            {
                _multiplyValue = 0;
            }

            _multiplyValue++;
            OnMultiplyEvent?.Invoke(_multiplyValue);
        }
    }
}
