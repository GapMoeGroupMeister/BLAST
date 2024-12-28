using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AssignEnumToEnum", story: "Assign [Enum1] to [Enum2]", category: "Action", id: "4f1850014f88be5355fa7faaf439b951")]
public partial class AssignEnumToEnumAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyState> Enum1;
    [SerializeReference] public BlackboardVariable<EnemyState> Enum2;
    [SerializeReference] public BlackboardVariable<ChangeStateEvent> Event;

    protected override Status OnStart()
    {
        Enum2.Value = Enum1.Value;
        return Status.Success;
    }
}

