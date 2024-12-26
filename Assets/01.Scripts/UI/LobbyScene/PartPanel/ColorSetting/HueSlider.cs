using System;
using UnityEngine;
using UnityEngine.UI;
namespace LobbyScene.ColorSettings
{
    public class HueSlider : MonoBehaviour
    {

        public event Action<float> OnHueValueChanged;
        private Slider _slider;
        [SerializeField] private Color _hueColor;
        private float _hueValue;

        public Color HueColor => _hueColor;
        public float HueValue => _hueValue;


        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(HandleHueValueChanged);
        }

        public void SetHueValue(float value)
        {
            _slider.value = value;
            _hueValue = value;
        }


        private void HandleHueValueChanged(float hue)
        {
            _hueValue = hue;
            _hueColor = Color.HSVToRGB(hue, 1f, 1f);
            OnHueValueChanged?.Invoke(_hueValue);
        }

    }
}