using System;
using DG.Tweening;
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
        public event Action OnSelectConfirmEvent;
        private Action OnDataChanged;
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private TextMeshProUGUI _colorSetNameText;
        [SerializeField] private GameObject _selectMarker;
        [SerializeField] private RectTransform _colorTypeSelecterTrm;

        [Header("ColorSet Slots")]
        [SerializeField] private ColorTypeSlot[] _colorSlots;
        private ColorSettingData _data;
        public ColorSettingData data => _data;


        private ColorTypeSlot _currentColorType;

        private bool _isActive;
        public bool IsActive => _isActive;


        private void Awake()
        {
            _nameInputField.onValueChanged.AddListener(HandleTextChanged);
        }

        private void HandleTextChanged(string newName)
        {
            _data.colorSetName = newName;
        }

        public void Initialize(ColorSettingData data)
        {
            _data = data;
            OnDataChanged?.Invoke();

            for (int i = 0; i < _colorSlots.Length; i++)
            {
                ColorTypeSlot slot = _colorSlots[i];
                slot.Initialize(i);
                slot.SetColor(data.colors[i]);
                _nameInputField.text = data.colorSetName;
                slot.OnClickEvent += HandleSelectColorType;
                slot.OnColorChanged += HandleColorChanged;
            }
        }

        private void HandleColorChanged(int index, Color color)
        {
            _data.colors[index] = color;
        }

        private void HandleSelectColorType(ColorTypeSlot slot)
        {
            OnColorTypeSelectEvent?.Invoke(slot);
            _currentColorType = slot;
            _colorTypeSelecterTrm.DOAnchorPosX(slot.RectTrm.anchoredPosition.x, 0.1f);
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
            HandleSelectColorType(_colorSlots[0]);
        }

        public void HandleSelectConfirm()
        {
            OnSelectConfirmEvent?.Invoke();
        }

        public void HandleEditorMode()
        {
            _colorTypeSelecterTrm.gameObject.SetActive(true);
        }

        public void HandleDisableEditorMode()
        {
            _colorTypeSelecterTrm.gameObject.SetActive(false);
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