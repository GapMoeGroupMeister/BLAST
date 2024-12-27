using System;
using Crogen.PowerfulInput;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LobbyScene.ColorSettings
{

    public class ColorPicker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private InputReader _input;
        [SerializeField] private HueSlider _hueSlider;
        [SerializeField] private Slider _saturationSlider;
        [SerializeField] private Slider _valueSlider; // 
        [SerializeField] private RectTransform _pickingPanelTrm;
        [SerializeField] private RectTransform _pickerTrm;
        private Image _image;
        private Image _pickerImage;
        private Material _pickingPanelMaterial;
        private Material _pickerMaterial;
        private int _hueColorHash;
        private int _pickerColorHash;
        Action<float, float, float> OnPickerChangeEvent;// S, V
        [Header("Current Status")]
        [SerializeField] private Color _currentColor;
        private bool _isClick;

        // HSV Color Levels
        private float _hueLevel;
        private float _saturationLevel;
        private float _valueLevel;


        protected void Awake()
        {
            _image = _pickingPanelTrm.GetComponent<Image>();
            _pickingPanelMaterial = _image.material;
            _hueColorHash = Shader.PropertyToID("_Color");

            _pickerImage = _pickerTrm.GetComponent<Image>();
            _pickerMaterial = _pickerImage.material;
            _pickerColorHash = Shader.PropertyToID("_HueColor");

            _hueSlider.OnHueValueChanged += HandleHueChanged;
            _saturationSlider.onValueChanged.AddListener(HandleSaturationChanged);
            _valueSlider.onValueChanged.AddListener(HandleValueChanged);

            OnPickerChangeEvent += HandleChangedColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {

        }

        public void SetColor(Color newColor)
        {
            Color.RGBToHSV(newColor, out _hueLevel, out _saturationLevel, out _valueLevel);

            _hueSlider.SetHueValue(_hueLevel);
            _saturationSlider.value = _saturationLevel;
            _valueSlider.value = _valueLevel;
            InvokeColorChanged();
        }

        // Event Funcs
        private void HandleHueChanged(float value)
        {
            _hueLevel = value;
            _pickingPanelMaterial.SetColor(_hueColorHash, _hueSlider.HueColor);
            InvokeColorChanged();
        }
        private void HandleSaturationChanged(float value)
        {
            _saturationLevel = value;
            SetPickerPosition(value * 400f, _pickerTrm.anchoredPosition.y);
            InvokeColorChanged();
        }
        private void HandleValueChanged(float value)
        {

            _valueLevel = value;
            SetPickerPosition(_pickerTrm.anchoredPosition.x, value * 400f);

            InvokeColorChanged();
        }
        private void InvokeColorChanged()
        {
            OnPickerChangeEvent?.Invoke(_hueLevel, _saturationLevel, _valueLevel);
        }

        private void SetPickerPosition(float x, float y)
        {
            _pickerTrm.anchoredPosition = new Vector2(Mathf.Clamp(x, 0f, 400f), Mathf.Clamp(y, 0f, 400f));
        }


        // ===

        public void HandleChangedColor(float h, float s, float v)
        {
            _currentColor = Color.HSVToRGB(h, s, v);
            _pickerMaterial.SetColor(_pickerColorHash, _currentColor);
        }


        // Pointer Events

        private void LateUpdate()
        {
            if (!_isClick) return;

            Vector2 mousePos = _input.MousePosition;
            Vector2 pickerPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_pickingPanelTrm, mousePos, null, out pickerPos);
            SetPickerPosition(pickerPos.x, pickerPos.y);

            float newSaturation = pickerPos.x / 400f;
            float newValue = pickerPos.y / 400f;

            _saturationSlider.value = newSaturation;
            _valueSlider.value = newValue;
            OnPickerChangeEvent?.Invoke(_hueLevel, newSaturation, newValue);
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            _isClick = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isClick = true;
        }


    }

}