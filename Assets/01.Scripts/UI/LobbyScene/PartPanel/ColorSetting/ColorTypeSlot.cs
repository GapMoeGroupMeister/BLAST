using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LobbyScene.ColorSettings
{

    public class ColorTypeSlot : MonoBehaviour, IPointerClickHandler
    {

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
            OnClickEvent?.Invoke(this);
        }
    }

}