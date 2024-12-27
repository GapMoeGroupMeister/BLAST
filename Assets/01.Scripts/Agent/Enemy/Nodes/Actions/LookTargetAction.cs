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
        Face.Value.LookAt(Target.Value);
        return Status.Success;
    }
}

