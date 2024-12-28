using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CastDamage", story: "Damage Casted by [Owner]", category: "Action", id: "4001c6087c5c1e0f5fcf6eba56e80313")]
public partial class CastDamageAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Owner;

    protected override Status OnStart()
    {
        Owner.Value.CastDamage();
        return Status.Success;
    }
}

