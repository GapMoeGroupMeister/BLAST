using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LobbyScene.ColorSettings
{

    public class ColorSettingSlot : MonoBehaviour, IPointerClickHandler
    {
        public event Action<ColorSettingSlot> OnSelectEvent;
        public event Action OnCancleEvent;
        private Action OnDataChanged;
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private TextMeshProUGUI _colorSetNameText;
        [SerializeField] private GameObject _selectMarker;

        [Header("ColorSet Slots")]
        [SerializeField] private ColorTypeSlot _colorSlot1;
        [SerializeField] private ColorTypeSlot _colorSlot2;
        [SerializeField] private ColorTypeSlot _colorSlot3;
        [SerializeField] private ColorTypeSlot _colorSlot4;
        private ColorSettingData _data;
        public ColorSettingData data => _data;

        private bool _isActive;
        public bool IsActive => _isActive;

        public void Initialize(ColorSettingData data)
        {
            _data = data;
            OnDataChanged?.Invoke();
            _colorSlot1.SetColor(data.color1);
            _colorSlot2.SetColor(data.color2);
            _colorSlot3.SetColor(data.color3);
            _colorSlot4.SetColor(data.lightColor);
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            _isActive = !_isActive;

            if (_isActive)
            {
                HandleOpenColorSet();
            }
            else
            {
                HandleCloseColorSet();
            }

        }

        public void HandleOpenColorSet()
        {
            OnSelectEvent?.Invoke(this);
        }


        public void HandleCloseColorSet()
        {
            // 데이터 저장 로직이 들어가야됨
            // 아마 이를 구독한 패널쪽에서 해줄것 같음.
            OnCancleEvent?.Invoke();
        }

        public void DestroySlot()
        {
            Destroy(gameObject);
        }

    }

}