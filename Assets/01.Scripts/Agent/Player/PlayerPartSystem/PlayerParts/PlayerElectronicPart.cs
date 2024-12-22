using DG.Tweening;
using UnityEngine;

public class PlayerElectronicPart : PlayerPart
{
    [SerializeField] private Transform _subEngineRTrm;
    [SerializeField] private Transform _subEngineLTrm;

    [SerializeField] private float _subEngineNeglectedDelayTime = 3.0f;
    
    private float _subEngineLNeglectedCurrentTime = 0;
    private float _subEngineRNeglectedCurrentTime = 0;
    
    private Sequence _subEngineLSequence;
    private Sequence _subEngineRSequence;

    private void Awake()
    {
        _subEngineLSequence = DOTween.Sequence();
        _subEngineRSequence = DOTween.Sequence();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void HandleAttackUpdateL(bool isAttack)
    {
        _subEngineLSequence?.Kill();
        _subEngineLSequence.Append(_subEngineLTrm.DOLocalMoveX(-3.6f, 0.1f).OnComplete(() =>
        {
            base.HandleAttackUpdateL(isAttack);
        }));
    }

    protected override void HandleAttackUpdateR(bool isAttack)
    {
        _subEngineRSequence?.Kill();
        _subEngineRSequence.Append(_subEngineRTrm.DOLocalMoveX(3.6f, 0.1f).OnComplete(() =>
        {
            base.HandleAttackUpdateR(isAttack);
        }));
    }

    public void SubEngineLDisable()
    {
        _subEngineLSequence?.Kill();
        _subEngineLSequence.Append(_subEngineLTrm.DOLocalMoveX(-2.52f, 1f));   
    }

    public void SubEngineRDisable()
    {
        _subEngineRSequence?.Kill();
        _subEngineRSequence.Append(_subEngineRTrm.DOLocalMoveX(2.52f, 1f));   
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
