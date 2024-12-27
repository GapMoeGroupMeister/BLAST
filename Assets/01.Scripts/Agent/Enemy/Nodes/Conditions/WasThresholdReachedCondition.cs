using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "wasThresholdReached", story: "Was [Threshold] Reached by [Timer]", category: "Conditions", id: "ac442f2d4f669befcfdb7726e93a0127")]
public partial class WasThresholdReachedCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> Threshold;
    [SerializeReference] public BlackboardVariable<float> Timer;

    public override bool IsTrue()
    {
        bool isTrue = Timer.Value + Threshold.Value < Time.time;
        if (isTrue)
        {
            Timer.Value = Time.time;
        }
        return isTrue;
    }
}
