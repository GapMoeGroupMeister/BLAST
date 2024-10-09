using DG.Tweening;
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

    public void SetUI()
    {
        _levelText.text = $"Level {XPManager.Instance.GetLevel.ToString()}";
        _coinText.text = ResourceManager.Instance.GetCoin().ToString();
        _servivedText.text = TimeManager.Instance.CurrentGlobalTimerString;
    }

    public override void Open()
    {
        Sequence seq = DOTween.Sequence();
        base.Open();
        SetUI();
        
        TimeManager.Instance.PauseTime();
    }
    
    
}