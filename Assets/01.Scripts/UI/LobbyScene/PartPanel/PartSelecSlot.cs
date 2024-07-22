using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PartSelecSlot : MonoBehaviour
{
    public PlayerPartDataSO partSO;
    [SerializeField] private Image _partImage;
    [SerializeField] private TextMeshProUGUI _partNameText;
    
    public void Initialize(PlayerPartDataSO data)
    {
        partSO = data;
        
    }

    public void Refresh()
    {
        _partImage.sprite = partSO.partImage;
        _partNameText.text = partSO.partName;
        
    }
}
