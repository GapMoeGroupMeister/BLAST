using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Crogen.CrogenPooling;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PushOwner", story: "Push [Owner]", category: "Action", id: "5a00d97cdeb50ec58ba5a9546ca8e20e")]
public partial class PushOwnerAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Owner;

    protected override Status OnStart()
    {
        Debug.Log("PUSH");
        Owner.Value.Push();
        return Status.Success;
    }
}

