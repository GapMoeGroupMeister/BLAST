using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Crogen.PowerfulInput
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Crogen/InputReader", order = 0)]
    public class InputReader : ScriptableObject, Controls.IPlayerActions
    {
        #region Input Event

        public event Action<Vector3> MoveEvent;
        public event Action DashEvent;
        public event Action AttackEvent;
        public event Action<Vector2> AttackDirectionEvent;
        #endregion

        private Controls _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if(context.performed)
                DashEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector3>());
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
                AttackEvent?.Invoke();
        }

        public void OnAttackDirection(InputAction.CallbackContext context)
        {
            AttackDirectionEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }
}