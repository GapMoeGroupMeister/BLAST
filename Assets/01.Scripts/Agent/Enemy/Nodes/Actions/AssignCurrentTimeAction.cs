using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AssignCurrentTime", story: "Assign Current Time to [Variable]", category: "Action", id: "a6b5f2151afc711ef8c13967f63028f5")]
public partial class AssignCurrentTimeAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Variable;

    protected override Status OnStart()
    {
        Variable.Value = Time.time;
        return Status.Success;
    }
}

