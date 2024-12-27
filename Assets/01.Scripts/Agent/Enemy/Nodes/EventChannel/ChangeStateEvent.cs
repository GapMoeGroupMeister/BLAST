using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/ChangeStateEvent")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "ChangeStateEvent", message: "Change State To [State]", category: "Events", id: "1ed0e44891862df2650e65e5a072f7b0")]
public partial class ChangeStateEvent : EventChannelBase
{
    public delegate void ChangeStateEventEventHandler(EnemyState State);
    public event ChangeStateEventEventHandler Event; 

    public void SendEventMessage(EnemyState State)
    {
        Event?.Invoke(State);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<EnemyState> StateBlackboardVariable = messageData[0] as BlackboardVariable<EnemyState>;
        var State = StateBlackboardVariable != null ? StateBlackboardVariable.Value : default(EnemyState);

        Event?.Invoke(State);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        ChangeStateEventEventHandler del = (State) =>
        {
            BlackboardVariable<EnemyState> var0 = vars[0] as BlackboardVariable<EnemyState>;
            if(var0 != null)
                var0.Value = State;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as ChangeStateEventEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as ChangeStateEventEventHandler;
    }
}

