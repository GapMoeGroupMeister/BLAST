using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PartSelectSlot : MonoBehaviour
{
    public PlayerPartDataSO partSO;
    [SerializeField] private Image _partImage;
    [SerializeField] private TextMeshProUGUI _partNameText;
    [SerializeField] private Image _selectIcon;
    [SerializeField] private Button _button;

    public void AddOnClieckEvent(UnityAction action) => _button.onClick.AddListener(action);

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

        _selectIcon.gameObject.SetActive(SaveManager.Instance.GetCurrentPlayerPart() == partSO.id);
    }

    public void PartSelect()
    {
        PartChanger.Instance.ChangePart(partSO);
        LobbySceneUIManager.Instance.RefreshSelectPartInfo(partSO);
        SaveManager.Instance.SelectPlayerPart(partSO.id);
    }
}
