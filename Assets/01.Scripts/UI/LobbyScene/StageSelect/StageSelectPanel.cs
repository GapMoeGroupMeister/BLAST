using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StageDifficulty
{
    Easy = 1,
    Normal,
    Hard,
    Impossible
}

public class StageSelectPanel : UIPanel
{
    [SerializeField] private StageInfoListSO _infoList;
    [SerializeField] private StageSelectSlot _slotPrefab;
    [SerializeField] private TextMeshProUGUI _stageDescriptionText;
    [SerializeField] private TextMeshProUGUI _stageDifficultyText;
    [SerializeField] private Image _diffucultyGauge;
    [SerializeField] private RectTransform _contentTrm;
    private readonly string _baseDifficultyText = "<color=white>위험도 </color>";
    private RectTransform _rectTrm;
    [Header("Setting")]
    [SerializeField] private Color _difficultyStartColor;
    [SerializeField] private Color _difficultyEndColor;
    [SerializeField] private float _activeXDelta;
    [SerializeField] private float _defaultXDelta;
    [SerializeField] private float _duration = 0.15f;
    private bool _isActive;
    protected override void Awake()
    {
        base.Awake();
        _rectTrm = transform as RectTransform;
        Initialize();
    }

    public override void Open()
    {
        if(_isActive) return;
        SetCanvasActive(true);
        UIControlManager.Instance.overUIAmount ++;
        _rectTrm.DOAnchorPosX(_activeXDelta, _duration).OnComplete(() => _isActive = true);
    }

    public override void Close()
    {
        if(!_isActive) return;
         SetCanvasActive(false);
         UIControlManager.Instance.overUIAmount--;
        _rectTrm.DOAnchorPosX(_defaultXDelta, _duration).OnComplete(() => _isActive = false);
    }

    public void SelectStage(StageInfoSO stageInfo)
    {
        _stageDescriptionText.text = stageInfo.stageDescription;
        _stageDifficultyText.text = $"{_baseDifficultyText}{stageInfo.stageDifficulty.ToString()}";
        _diffucultyGauge.DOFillAmount((int)stageInfo.stageDifficulty * 0.25f, _duration);
        Color difficultyColor = Color.Lerp(_difficultyStartColor, _difficultyEndColor, (int)stageInfo.stageDifficulty * 0.25f);
        _stageDifficultyText.color = difficultyColor;
        _diffucultyGauge.color = difficultyColor;
    }

    private void Initialize()
    {
        foreach (StageInfoSO info in _infoList.list)
        {
            StageSelectSlot slot = Instantiate(_slotPrefab, _contentTrm);
            slot.Initialize(this, info);

        }
    }
}
