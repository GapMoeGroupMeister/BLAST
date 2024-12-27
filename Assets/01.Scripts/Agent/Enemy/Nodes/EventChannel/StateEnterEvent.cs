using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/StateEnterEvent")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "StateEnterEvent", message: "On [StateEnum] State Enter", category: "Events", id: "7b65c67dab687b92849747d53d93b825")]
public partial class StateEnterEvent : EventChannelBase
{
    public delegate void StateEnterEventEventHandler(EnemyState StateEnum);
    public event StateEnterEventEventHandler Event; 

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
        StateEnterEventEventHandler del = (StateEnum) =>
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
        Event += del as StateEnterEventEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as StateEnterEventEventHandler;
    }
}

