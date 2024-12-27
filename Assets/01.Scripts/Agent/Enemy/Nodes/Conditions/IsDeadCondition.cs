using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsDead", story: "Is [Health] Dead", category: "Conditions", id: "0f97d77b1a1139d2e9601958fe59bf77")]
public partial class IsDeadCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Health> Health;

    public override bool IsTrue()
    {
        return Health.Value.IsDead;
    }
}
