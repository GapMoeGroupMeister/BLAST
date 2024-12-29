using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AssignVariable", story: "Assign [Variable1] to [Variable2]", category: "Action", id: "c6ab64e1bcfed5ae3197550e5e03f70e")]
public partial class AssignVariableAction : Action
{
    [SerializeReference] private BlackboardVariable<object> Variable1;
    [SerializeReference] private BlackboardVariable<object> Variable2;
    protected override Status OnStart()
    {
        Variable1.Value = Variable2.Value;
        return Status.Success;
    }
}

