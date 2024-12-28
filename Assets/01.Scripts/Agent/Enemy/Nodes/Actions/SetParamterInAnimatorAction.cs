using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Paramter In Animator", story: "Set [Paramter] in [Animator] [Condition]", category: "Action", id: "c62635e3f0eb7e60675a8dddb37e7f21")]
public partial class SetParamterInAnimatorAction : Action
{
    [SerializeReference] public BlackboardVariable<string> Paramter;
    [SerializeReference] public BlackboardVariable<Animator> Animator;
    [SerializeReference] public BlackboardVariable<bool> Condition;

    protected override Status OnStart()
    {
        Animator.Value.SetBool(Paramter.Value, Condition.Value);
        return Status.Success;
    }
}

