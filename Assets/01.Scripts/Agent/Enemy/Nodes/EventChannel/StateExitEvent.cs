using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/StateExitEvent")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "StateExitEvent", message: "On [StateEnum] State Exit", category: "Events", id: "43b709a983a7132c4545b249a0e79b9a")]
public partial class StateExitEvent : EventChannelBase
{
    public delegate void StateExitEventEventHandler(EnemyState StateEnum);
    public event StateExitEventEventHandler Event; 

    public void SendEventMessage(EnemyState StateEnum)
    {
        Event?.Invoke(StateEnum);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<EnemyState> StateEnumBlackboardVariable = messageData[0] as BlackboardVariable<EnemyState>;
        var StateEnum = StateEnumBlackboardVariable != null ? StateEnumBlackboardVariable.Value : default(EnemyState);

        Event?.Invoke(StateEnum);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        StateExitEventEventHandler del = (StateEnum) =>
        {
            BlackboardVariable<EnemyState> var0 = vars[0] as BlackboardVariable<EnemyState>;
            if(var0 != null)
                var0.Value = StateEnum;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as StateExitEventEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as StateExitEventEventHandler;
    }
}

