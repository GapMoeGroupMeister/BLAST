using DG.Tweening;
using UnityEngine;

public class PlayerElectronicPart : PlayerPart
{
    [SerializeField] private Transform _subEngineRTrm;
    [SerializeField] private Transform _subEngineLTrm;

    [SerializeField] private float _subEngineNeglectedDelayTime = 3.0f;
    
    private float _subEngineLNeglectedCurrentTime = 0;
    private float _subEngineRNeglectedCurrentTime = 0;
    
    private Tween _subEngineLTween;
    private Tween _subEngineRTween;

    private void Awake()
    {
        _subEngineLTween = DOTween.Sequence();
        _subEngineRTween = DOTween.Sequence();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void HandleAttackUpdateL(bool isAttack)
    {
        _subEngineLTween?.Pause();
        _subEngineLTween?.Kill();
        _subEngineLTween = _subEngineLTrm.DOLocalMoveX(-3.6f, 0.1f).OnComplete(() =>
        {
            base.HandleAttackUpdateL(isAttack);
        });
    }

    protected override void HandleAttackUpdateR(bool isAttack)
    {
        _subEngineRTween?.Pause();
        _subEngineRTween?.Kill();
        _subEngineRTween = _subEngineRTrm.DOLocalMoveX(3.6f, 0.1f).OnComplete(() =>
        {
            base.HandleAttackUpdateR(isAttack);
        });
    }

    private void SubEngineLDisable()
    {
        _subEngineLTween?.Pause();
        _subEngineLTween?.Kill();
        _subEngineLTween = _subEngineLTrm.DOLocalMoveX(-2.52f, 1f);   
    }

    private void SubEngineRDisable()
    {
        _subEngineRTween?.Pause();
        _subEngineRTween?.Kill();
        _subEngineRTween = _subEngineRTrm.DOLocalMoveX(2.52f, 1f);   
    }
    
    protected override void Update()
    {
        base.Update();
        _subEngineLNeglectedCurrentTime += Time.deltaTime;
        _subEngineRNeglectedCurrentTime += Time.deltaTime;


        if (_subEngineRNeglectedCurrentTime > _subEngineNeglectedDelayTime)
        {
            _subEngineRNeglectedCurrentTime = 0;
            SubEngineRDisable();
        }

        if (_subEngineLNeglectedCurrentTime > _subEngineNeglectedDelayTime)
        {
            _subEngineLNeglectedCurrentTime = 0;
            SubEngineLDisable();
        }
    }
}
