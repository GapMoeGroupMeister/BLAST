using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LobbyScene.ColorSettings
{

    public class ColorSettingSlot : MonoBehaviour, IPointerClickHandler
    {

        [SerializeField] private TextMeshProUGUI _colorSetNameText;

        [SerializeField] private Image _color1Image;
        [SerializeField] private Image _color2Image;
        [SerializeField] private Image _color3Image;
        [SerializeField] private Image _color4Image;
        private ColorSettingData _data;
        

        

        public void OnPointerClick(PointerEventData eventData)
        {

            
        }
    }

}