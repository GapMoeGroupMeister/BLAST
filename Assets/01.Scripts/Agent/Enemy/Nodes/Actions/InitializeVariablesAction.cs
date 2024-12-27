using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "InitializeVariables", story: "Initialize Variables From [Self]", category: "Action", id: "d041e6a90b4119190b17d99ee71897e5")]
public partial class InitializeVariablesAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Self;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

