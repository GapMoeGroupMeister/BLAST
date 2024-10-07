using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClearPanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _servivedText;
    [SerializeField] private TextMeshProUGUI _rankText;
    
    [SerializeField] private Button _gameEndButton;
    
    [SerializeField] private Transform _upgradesParent;

    protected override void Awake()
    {
        base.Awake();
        _gameEndButton.onClick.AddListener(GameEnd);
    }

    private void GameEnd()
    {
        ResourceManager.Instance.SaveCoin();
        SceneLoadingManager.LoadScene("LobbyScene");
    }

    public void SetUI(LevelData data)
    {
        _levelText.text = $"Level {data.level}";
        _coinText.text = data.coin.ToString();
        _servivedText.text = data.servivalTime;
        _rankText.text = data.rank;
    }

    public override void Open()
    {
        base.Open();
        
        _levelText.text = $"Level {XPManager.Instance.GetLevel.ToString()}";
        _coinText.text = ResourceManager.Instance.GetCoin().ToString();
        _servivedText.text = TimeManager.Instance.CurrentGlobalTimerString;
        TimeManager.Instance.PauseTime();
    }
    
    
}