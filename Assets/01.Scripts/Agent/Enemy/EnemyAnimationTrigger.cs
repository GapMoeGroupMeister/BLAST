using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationTriggerEnum
{
    EndTrigger = 1,
    EventTrigger = 2,
    AttackTrigger = 4,
    EffectTrigger = 8
}

public class EnemyAnimationTrigger : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemy;

    private void AnimationTrigger(AnimationTriggerEnum triggerBit)
    {
        _enemy.AnimationEndTrigger(triggerBit);
    }
}
