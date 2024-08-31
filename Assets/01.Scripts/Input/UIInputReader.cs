using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "UIInputReader", menuName = "SO/UI_InputReader")]
public class UIInputReader : ScriptableObject, Controls.IUIActions
{
    public event Action OnEscEvent;
    public event Action OnMultiplyEvent;

    private Controls _controls;

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
        _controls.Disable();
    }


    public void OnEsc(InputAction.CallbackContext context)
    {
        OnEscEvent?.Invoke();
    }
}
