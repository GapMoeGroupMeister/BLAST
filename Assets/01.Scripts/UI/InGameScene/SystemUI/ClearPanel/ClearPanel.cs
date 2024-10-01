using TMPro;
using UnityEngine;

public class ClearPanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _servivedText;
    [SerializeField] private TextMeshProUGUI _rankText;
    
    [SerializeField] private Transform _upgradesParent;
}