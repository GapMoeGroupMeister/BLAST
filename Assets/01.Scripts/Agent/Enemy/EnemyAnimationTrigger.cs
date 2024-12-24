using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum AnimationTriggerEnum
{
    EndTrigger = 1,
    EventTrigger = 2,
    AttackTrigger = 4,
    EffectTrigger = 8
}

public class EnemyAnimationTrigger : MonoBehaviour
{
    public event Action<AnimationTriggerEnum> OnAnimationTriggerEvent;

    private void AnimationTrigger(AnimationTriggerEnum triggerBit)
    {
        OnAnimationTriggerEvent?.Invoke(triggerBit);
    }
}
