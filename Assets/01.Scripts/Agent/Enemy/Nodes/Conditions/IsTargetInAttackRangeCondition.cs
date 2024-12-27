using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsTargetInAttackRange", story: "Is [Target] in Attack [Range] from [Owner]", category: "Conditions", id: "2309ab971d8a39dfd7b43355752fe2d2")]
public partial class IsTargetInAttackRangeCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Range;
    [SerializeReference] public BlackboardVariable<Enemy> Owner;

    public override bool IsTrue()
    {
        Vector3 startPos = Owner.Value.transform.position;
        Vector3 targetPos = Target.Value.position;
        float distance = Vector3.Distance(startPos, targetPos);
        if(distance <= Range.Value)
        {
            Vector3 direction = targetPos - startPos;
            return Physics.Raycast(startPos, direction.normalized, distance, Owner.Value.whatIsObstacle);
        }
        return false;
    }
}
