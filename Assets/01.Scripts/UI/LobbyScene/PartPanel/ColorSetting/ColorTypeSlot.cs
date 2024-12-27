using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LobbyScene.ColorSettings
{

    public class ColorTypeSlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _colorTypeText;
        public event Action<int, Color> OnColorChanged;
        public event Action<ColorTypeSlot> OnClickEvent;
        private Image _image;
        private Color _color;
        public Color Color => _color;
        private int _index;
        private RectTransform _rectTrm;
        public RectTransform RectTrm => _rectTrm;

        private void Awake()
        {
            _rectTrm = transform as RectTransform;
            _image = GetComponent<Image>();

        }

        public void Initialize(int index)
        {
            _index = index;
        }


        public void SetColor(Color color)
        {
            _color = color;
            _image.color = color;
            OnColorChanged?.Invoke(_index, color);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickEvent?.Invoke(this); // Slot에서 함수가 여기 구독되어있음
        }
    }

}