using System;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LobbyScene
{
    
    public class PartSelectSlot : MonoBehaviour
    {
        public event Action<PlayerPartDataSO> OnSelectEvent;
        public PlayerPartDataSO partSO;
        [SerializeField] private Image _partImage;
        [SerializeField] private TextMeshProUGUI _partNameText;
        [SerializeField] private Image _selectIcon;
        [SerializeField] private Button _button;

        public int dataId => partSO.id;
    

        private void Awake() {
            
            _button.onClick.AddListener(PartSelect);
        }

        public void Initialize(PlayerPartDataSO data)
        {
            partSO = data;
            _partNameText.text = data.partName;
            Refresh();
        }

        public void Refresh()
        {
            _partImage.sprite = partSO.partImage;
            _partNameText.text = partSO.partName;

        }

        public void PartSelect()
        {
            OnSelectEvent?.Invoke(partSO);
        }

        public void SetSelectIcon(bool value)
        {
            _selectIcon.enabled = value;
        }
    }

}