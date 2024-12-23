using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEventSystem
{

    public interface IGameEventData
    {
        //empty class
    }

    [CreateAssetMenu(fileName = "GameEventDataChannelSO", menuName = "SO/Events/GameEventDataChannelSO")]
    public class GameEventChannelSO : ScriptableObject
    {
        private Dictionary<Type, Action<IGameEventData>> _events = new();
        private Dictionary<Delegate, Action<IGameEventData>> _lookUp = new();

        public void AddListener<T>(Action<T> handler) where T : IGameEventData
        {
            if (_lookUp.ContainsKey(handler) == false)  //이미 구독중인 매서드는 다시 구독되지 않도록
            {
                //타입캐스트 핸들러 작성
                Action<IGameEventData> castHandler = (evt) => 
                {
                    if(evt is T tEvent)
                    {
                        handler(tEvent);
                    }
                    //handler(evt as T);
                };
                _lookUp[handler] = castHandler;

                Type evtType = typeof(T);
                if (_events.ContainsKey(evtType))
                {
                    _events[evtType] += castHandler;
                }
                else
                {
                    _events[evtType] = castHandler;
                }
            }
            else
            {
                Debug.Log("이미 구독중");
            }
        }

        public void RemoveListener<T>(Action<T> handler) where T : IGameEventData
        {
            Type evtType = typeof(T);
            if (_lookUp.TryGetValue(handler, out Action<IGameEventData> action))
            {
                if (_events.TryGetValue(evtType, out Action<IGameEventData> internalAction))
                {
                    internalAction -= action;
                    if (internalAction == null)
                        _events.Remove(evtType);
                    else
                        _events[evtType] = internalAction;
                }
                _lookUp.Remove(handler);
            }
        }

        public void RaiseEvent(IGameEventData evt)
        {
            if (_events.TryGetValue(evt.GetType(), out Action<IGameEventData> handlers))
            {
                handlers?.Invoke(evt);
            }
        }

        public void Clear()
        {
            _events.Clear();
            _lookUp.Clear();
        }
    }
}