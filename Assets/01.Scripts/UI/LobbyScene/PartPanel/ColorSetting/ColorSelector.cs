using System;
using UnityEngine;
namespace LobbyScene.ColorSettings
{
    public class ColorSelector : MonoBehaviour
    {
        [SerializeField] private ColorPicker _colorPicker;
        private ColorTypeSlot _currentColorType;

        public void SetColorType(ColorTypeSlot slot)
        {
            _currentColorType = slot;
            _colorPicker.SetColor(slot.Color);
        }

        private void Awake()
        {

            _colorPicker.OnColorChangedEvent += HandleColorChanged;
        }

        private void HandleColorChanged(Color color)
        {
            if(_currentColorType == null) return;
            _currentColorType.SetColor(color);
        }
    }
}