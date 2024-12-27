using System.Collections.Generic;
using EasySave.Json;
using UnityEngine;
using UnityEngine.UI;
namespace LobbyScene.ColorSettings
{

    public class ColorSettingPanel : MonoBehaviour
    {
        [SerializeField] private ColorSettingSlot _colorSlotPrefab;


        [SerializeField] private ColorSetDetailWorkPanel _detailPanel;
        private ColorSelector _colorSelector;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _contentTrm;
        [SerializeField] private RectTransform _addColorButtonTrm;
        [SerializeField] private ColorPalette _colorPalette; // Picker와 등등의 것들을 들고있는놈
        [SerializeField] private ColorPicker _colorPicker;
        [SerializeField] private ColorSettingData _defaultData;

        private List<ColorSettingSlot> _slotList = new();
        [SerializeField]
        private List<ColorSettingData> datas;

        private ColorSettingSlot _currentSelectedSlot;

        private void Awake()
        {
            _colorSelector = GetComponent<ColorSelector>();
            Initialize();
            //datas = EasyToJson.ListFromJson<ColorSettingData>("ColorSet");

            _detailPanel.OnEditModeEvent += HandleEditMode;
            _detailPanel.OnDeleteColorSetEvent += HandleDeleteSlot;
        }

        private void Initialize()
        {
            if (datas == null)
            {
                Debug.LogError("Datas is Null");
                return;
            }

            for (int i = 0; i < datas.Count; i++)
            {
                ColorSettingSlot slot = AddColorSet();
                slot.Initialize(datas[i]);
                AddEventHandlersColorSettingSlot(slot);
            }
        }

        public ColorSettingSlot AddColorSet()
        {
            ColorSettingSlot slot = Instantiate(_colorSlotPrefab, _contentTrm);
            _addColorButtonTrm.SetAsLastSibling();
            return slot;
        }


        public void AddNewColorSet()
        {
            ColorSettingSlot slot = AddColorSet();
            ColorSettingData data = new ColorSettingData(_defaultData);

            _slotList.Add(slot);
            datas.Add(data);
            slot.Initialize(data);
            AddEventHandlersColorSettingSlot(slot);
        }

        private void AddEventHandlersColorSettingSlot(ColorSettingSlot slot)
        {
            slot.OnSelectEvent += HandleSelectColorSet;
            slot.OnCancleEvent += HandleUnSelectColorSet;
            slot.OnColorTypeSelectEvent += SetColorType;
        }

        private void HandleSelectColorSet(ColorSettingSlot slot)
        {
            _currentSelectedSlot = slot;
            _detailPanel.SetSlotParent(slot.transform);
            _scrollRect.enabled = false;
            SetActiveDetailPanel(true);

            // 나중에 이쪽에서 _currentSelectedSlot.data 가져와서
            // 색 변경 하셈ww
        }

        private void HandleUnSelectColorSet()
        {
            _scrollRect.enabled = true;
            // SetActiveDetailPanel(false);
            // SetActiveColorPicker(false);
        }



        private void HandleEditMode()
        {
            _colorPalette.transform.SetParent(_currentSelectedSlot.transform);
            SetActiveColorPicker(true);
        }

        private void HandleDeleteSlot()
        {
            SetActiveDetailPanel(false);
            SetActiveColorPicker(false);
            datas.Remove(_currentSelectedSlot.data);
            _slotList.Remove(_currentSelectedSlot);
            _currentSelectedSlot.DestroySlot();
        }


        private void SetActiveDetailPanel(bool value)
        {
            if (value)
                _detailPanel.Open();
            else
            {
                _detailPanel.SetCanvasActiveImmediately(false);
                _detailPanel.SetSlotParent(transform);
            }
        }

        private void SetActiveColorPicker(bool value)
        {
            if (value)
                _colorPalette.Open();
            else
            {
                _colorPalette.SetCanvasActiveImmediately(false);
                _colorPalette.transform.SetParent(transform);

            }
        }

        public void SetColorType(ColorTypeSlot colorType)
        {
            print("???");
            _colorSelector.SetColorType(colorType);
        }

    }
}