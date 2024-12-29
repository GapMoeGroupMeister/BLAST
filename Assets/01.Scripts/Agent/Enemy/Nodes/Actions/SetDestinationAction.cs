using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetDestination", story: "Set Destination to [Target] by [Movement]", category: "Action", id: "10a3f0c641e76623937f336a8cdd95d7")]
public partial class SetDestinationAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<EnemyMovement> Movement;

    protected override Status OnStart()
    {
        Movement.Value.SetDestination(Target.Value.transform.position);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

