using System;
using UnityEngine;
namespace LobbyScene.ColorSettings
{



    public class ColorSetDetailWorkPanel : UIPanel
    {

        public event Action OnSelectConfirmEvent;
        public event Action OnEditModeEvent;
        public event Action OnDeleteColorSetEvent;
        private RectTransform _rectTrm;


        protected override void Awake()
        {

            base.Awake();
            _rectTrm = transform as RectTransform;
        }

        public void SetSlotParent(Transform slotTrm)
        {
            transform.SetParent(slotTrm);
            _rectTrm.anchoredPosition = Vector2.zero;
        }

        public void HandleSelectConfirm() // Call By Buttons
        {
            OnSelectConfirmEvent?.Invoke();
        }


        public void HandleEditMode() // Call By Buttons
        {
            OnEditModeEvent?.Invoke();
        }

        public void HandleDeleteColorSet()
        {

            OnDeleteColorSetEvent?.Invoke();
        }
    }
}