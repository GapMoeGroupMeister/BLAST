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
        
        public Action<ColorTypeSlot> OnClickEvent;
        private Image _image;
        private Color _color;
        public Color Color => _color;

        private void Awake()
        {

            _image = GetComponent<Image>();

        }


        public void SetColor(Color color)
        {
            _color = color;
            _image.color = color;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickEvent?.Invoke(this); // Slot에서 함수가 여기 구독되어있음
        }
    }

}