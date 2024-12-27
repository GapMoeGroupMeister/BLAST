using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitForAnimatorTrigger", story: "Wait for [AnimatorTrigger] [Triggered]", category: "Action", id: "c5f338867dcc7a032e89be7cbac9aa9f")]
public partial class WaitForAnimatorTriggerAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyAnimatorTrigger> AnimatorTrigger;
    [SerializeReference] public BlackboardVariable<AnimatorTriggerEnum> Triggered;

    private bool _isTriggered = false;

    protected override Status OnStart()
    {
        _isTriggered = false;
        AnimatorTrigger.Value.OnAnimatorTriggerEvent += HandleOnAnimatorTriggerEvent;
        return Status.Running;
    }

    private void HandleOnAnimatorTriggerEvent(AnimatorTriggerEnum trigger)
    {
        if (trigger == Triggered.Value)
        {
            _isTriggered = true;
        }
    }

    protected override Status OnUpdate()
    {
        if (_isTriggered)
            return Status.Success;
        return Status.Running;
    }

    protected override void OnEnd()
    {
        AnimatorTrigger.Value.OnAnimatorTriggerEvent -= HandleOnAnimatorTriggerEvent;
    }
}

