using TMPro;
using UnityEngine;

public class ClearPanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _servivedText;
    [SerializeField] private TextMeshProUGUI _rankText;
    
    [SerializeField] private Transform _upgradesParent;
    
    public void SetUI(LevelData data)
    {
        _levelText.text = $"Level {data.level}";
        _coinText.text = data.coin.ToString();
        _servivedText.text = data.servivalTime;
        _rankText.text = data.rank;
    }
}