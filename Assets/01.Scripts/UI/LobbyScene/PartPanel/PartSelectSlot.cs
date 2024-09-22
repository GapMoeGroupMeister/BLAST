using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartSelectSlot : MonoBehaviour
{
    public PlayerPartDataSO partSO;
    [SerializeField] private Image _partImage;
    [SerializeField] private TextMeshProUGUI _partNameText;
    
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
        PartChanger.Instance.ChangePart(partSO);
        LobbySceneUIManager.Instance.RefreshSelectPartInfo(partSO);
        SaveManager.Instance.SelectPlayerPart(partSO.id);
        
    }
}
