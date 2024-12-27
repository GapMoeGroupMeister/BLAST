using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopImmediately", story: "Stop with [Movement]", category: "Action", id: "dd4009d76fce6a7716ef484cf303c542")]
public partial class StopImmediatelyAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyMovement> Movement;

    protected override Status OnStart()
    {
        Movement.Value.StopImmediately();
        return Status.Success;
    }
}

