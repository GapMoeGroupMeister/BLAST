using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

[BlackboardEnum]
public enum AnimatorTriggerEnum
{
    EndTrigger,
    EventTrigger,
    AttackTrigger,
    EffectTrigger
}

public class EnemyAnimatorTrigger : MonoBehaviour
{
    public event Action<AnimatorTriggerEnum> OnAnimatorTriggerEvent;

    private void AnimationTrigger(AnimatorTriggerEnum triggerBit)
    {
        OnAnimatorTriggerEvent?.Invoke(triggerBit);
    }
}
