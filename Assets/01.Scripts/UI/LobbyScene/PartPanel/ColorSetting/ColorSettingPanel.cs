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

        private ColorSetDataGroup _colorSetDataGroup;
        private List<ColorSettingSlot> _slotList = new();
        [SerializeField]

        private ColorSettingSlot _currentSelectedSlot;
        private readonly string path = "ColorSet";

        private void Awake()
        {
            _colorSelector = GetComponent<ColorSelector>();
            _colorSetDataGroup = EasyToJson.FromJson<ColorSetDataGroup>(path);
            // 처음 저장되어있던 색을 불러와야됨
            // 다만 가장 초기상태이거나 팔레트 정보가 없을떄는 강제로 팔레트를 하나 만들어주고
            // 그 데이터를 적용하는 방식으로 가야할 듯 함.
            if(_colorSetDataGroup.datas == null)
            {
                _colorSetDataGroup.datas = new List<ColorSettingData>();
            }

            _detailPanel.OnEditModeEvent += HandleEditMode;
            _detailPanel.OnDeleteColorSetEvent += HandleDeleteSlot;
            
            Initialize();
        }

        private void Initialize()
        {
            if (_colorSetDataGroup.datas == null)
            {
                Debug.LogError("Datas is Null");
                return;
            }

            for (int i = 0; i < _colorSetDataGroup.datas.Count; i++)
            {
                ColorSettingSlot slot = AddColorSet();
                slot.Initialize(_colorSetDataGroup.datas[i]);
                
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
            _colorSetDataGroup.datas.Add(data);
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

        public void HandleUnSelectColorSet()
        {
            _scrollRect.enabled = true;
            SetActiveDetailPanel(false);
            SetActiveColorPicker(false);
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
            _colorSetDataGroup.datas.Remove(_currentSelectedSlot.data);
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
            _colorSelector.SetColorType(colorType);
        }

        public void HandleSaveColorSetData()
        {
            if(_colorSetDataGroup == null) 
                _colorSetDataGroup = new ColorSetDataGroup();

            _colorSetDataGroup.currnetData = _currentSelectedSlot.data;

            EasyToJson.ToJson<ColorSetDataGroup>(_colorSetDataGroup, path, true);

        }

    }
}