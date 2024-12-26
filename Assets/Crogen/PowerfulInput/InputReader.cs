using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Crogen.PowerfulInput
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Crogen/InputReader", order = 0)]
    public class InputReader : ScriptableObject, Controls.IPlayerActions
    {
        #region Input Event

        public event Action MoveStartEvent;
        public event Action DashEvent;
        public event Action<float> ZoomEvent;

        public event Action<bool> AttackLEvent;
        public event Action<bool> AttackREvent;
        public event Action UseUltEvent;
        
        public Vector3 Movement { get; private set; }
        public Vector2 MousePosition { get; private set; }
        
        
        #endregion

        private Controls _controls;

        private void OnEnable()
        {
            
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            MoveStartEvent = null;
            DashEvent = null;
            ZoomEvent = null;
            AttackLEvent = null;
            AttackREvent = null; // 이벤트 초기화
            
            _controls.Disable();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if(context.performed)
                DashEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveStartEvent?.Invoke();
            Movement = context.ReadValue<Vector3>();
        }

        public void OnAttackDirection(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
        }

		public void OnZoom(InputAction.CallbackContext context)
		{
            ZoomEvent?.Invoke(Mathf.Clamp(context.ReadValue<float>(), -1, 1));
        }

        public void OnAttackL(InputAction.CallbackContext context)
        {
            if(context.started)
                AttackLEvent?.Invoke(true);
            if(context.canceled)
                AttackLEvent?.Invoke(false);
        }

        public void OnUseUlt(InputAction.CallbackContext context)
        {
            if(context.performed)
                UseUltEvent?.Invoke();
        }

        public void OnAttackR(InputAction.CallbackContext context)
        {
            if(context.started)
                AttackREvent?.Invoke(true);
            if(context.canceled)
                AttackREvent?.Invoke(false);
        }
    }
}