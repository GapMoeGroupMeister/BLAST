using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LobbyScene
{
    
    public class PartSelectSlot : MonoBehaviour
    {
        public PlayerPartDataSO partSO;
        [SerializeField] private Image _partImage;
        [SerializeField] private TextMeshProUGUI _partNameText;
        [SerializeField] private Image _selectIcon;
        [SerializeField] private Button _button;
        private PartSelectPanel _partSelectPanel;
    
        public void AddOnClieckEvent(UnityAction action) => _button.onClick.AddListener(action);

        public void Initialize(PartSelectPanel selectPanel, PlayerPartDataSO data)
        {
            partSO = data;
            _partSelectPanel = selectPanel;
            _partNameText.text = data.partName;
            Refresh();
        }

        public void Refresh()
        {
            _partImage.sprite = partSO.partImage;
            _partNameText.text = partSO.partName;

            //_selectIcon.gameObject.SetActive(SaveManager.Instance.GetCurrentPlayerPart() == partSO.id);
        }

        public void PartSelect()
        {
            _partSelectPanel.SelectPart(partSO);
            SaveManager.SelectPlayerPart(partSO.id);
        }

        public void SetSelectIcon(bool value)
        {
            _selectIcon.enabled = value;
        }
    }

}