using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EnumToString", story: "[Enum] to [String]", category: "Action", id: "a9a033ca2ba31e71ff44dbab7aab6e9b")]
public partial class EnumToStringAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyState> Enum;
    [SerializeReference] public BlackboardVariable<string> String;

    protected override Status OnStart()
    {
        String.Value = Enum.Value.ToString();
        return Status.Success;
    }
}

