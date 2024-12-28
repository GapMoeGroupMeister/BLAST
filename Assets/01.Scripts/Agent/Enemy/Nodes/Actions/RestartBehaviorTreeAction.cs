using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RestartBehaviorTree", story: "Restart [Owner]", category: "Action", id: "67316994c2712b4ba39eb24e13fa295e")]
public partial class RestartBehaviorTreeAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Owner;

    protected override Status OnStart()
    {
        Owner.Value.RestartBehaviorTree();
        return Status.Success;
    }
}

