using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Random = UnityEngine.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetRandomizeDestination", story: "Set Destination by [Radius] from [Target] with [Movement]", category: "Action", id: "800638da8676fae3c50661bd84c30c2e")]
public partial class SetRandomizeDestinationAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Radius;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<EnemyMovement> Movement;

    protected override Status OnStart()
    {
        if (!Movement.Value.NavAgent.isStopped)
            return Status.Success;
        Vector3 random = Random.insideUnitCircle;
        random.z = random.y;
        random.y = 0;
        random.Normalize();
        random *= Radius.Value;
        random += Target.Value.position;
        Movement.Value.SetDestination(random);
        return Status.Success;
    }
}

