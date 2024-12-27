using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LobbyScene.ColorSettings
{

    public class ColorSettingSlot : MonoBehaviour, IPointerClickHandler
    {
        public event Action<ColorTypeSlot> OnColorTypeSelectEvent;
        public event Action<ColorSettingSlot> OnSelectEvent;
        public event Action OnCancleEvent;
        private Action OnDataChanged;
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private TextMeshProUGUI _colorSetNameText;
        [SerializeField] private GameObject _selectMarker;

        [Header("ColorSet Slots")]
        [SerializeField] private ColorTypeSlot[] _colorSlots;
        private ColorSettingData _data;
        public ColorSettingData data => _data;


        private ColorTypeSlot _currentColorType;

        private bool _isActive;
        public bool IsActive => _isActive;

        public void Initialize(ColorSettingData data)
        {
            _data = data;
            OnDataChanged?.Invoke();

            for (int i = 0; i < _colorSlots.Length; i++)
            {
                ColorTypeSlot slot = _colorSlots[i];
                slot.SetColor(data.colors[i]);
                slot.OnClickEvent += HandleSelectColorType;
            }



        }

        private void HandleSelectColorType(ColorTypeSlot slot)
        {
            OnColorTypeSelectEvent?.Invoke(slot);
        }

        public void HandleToggleSlot()
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
            _currentColorType = _colorSlots[0];
            OnColorTypeSelectEvent?.Invoke(_currentColorType);
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

        public void OnPointerClick(PointerEventData eventData)
        {
            HandleToggleSlot();
        }
    }

}