using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LookTarget", story: "[Face] Looks at [Target]", category: "Action", id: "9d039ae3ed20ad83804f4976393628f3")]
public partial class LookTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Face;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    protected override Status OnStart()
    {
        Vector3 direction = Target.Value.position - Face.Value.position;
        direction.y = 0;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Face.Value.rotation = Quaternion.Euler(0, angle, 0);
        return Status.Success;
    }
}

