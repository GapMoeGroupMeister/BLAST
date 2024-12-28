using DG.Tweening;
using UnityEngine;

public class PlayerAPPart : PlayerPart
{
    [SerializeField] private Transform _subEngineRTrm;
    [SerializeField] private Transform _subEngineLTrm;

    protected override void HandleAttackUpdateL(bool isAttack)
    {
        MoveAttackSubEngineTrm(_subEngineLTrm, isAttack);
        base.HandleAttackUpdateL(isAttack);
    }

    protected override void HandleAttackUpdateR(bool isAttack)
    {
        MoveAttackSubEngineTrm(_subEngineRTrm, isAttack);
        base.HandleAttackUpdateR(isAttack);
    }

    private void MoveAttackSubEngineTrm(Transform target, bool isAttack)
    {
        target.DOPause();
        target.DOKill();
        float dir = target.localPosition.x < 0 ? -1 : 1;
        
        if (isAttack)
            target.DOLocalMoveX(3.6f * dir, 0.1f);
        else
            target.DOLocalMoveX(2.52f * dir, 1f);
    }
}
