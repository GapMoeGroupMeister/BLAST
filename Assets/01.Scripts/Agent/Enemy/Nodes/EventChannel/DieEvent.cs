using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/DieEvent")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "DieEvent", message: "Die Event", category: "Events", id: "fb80a48243150fe4c67bd71b42c0f834")]
public partial class DieEvent : EventChannelBase
{
    public delegate void DieEventEventHandler();
    public event DieEventEventHandler Event; 

    public void SendEventMessage()
    {
        Event?.Invoke();
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        Event?.Invoke();
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        DieEventEventHandler del = () =>
        {
            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as DieEventEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as DieEventEventHandler;
    }
}

