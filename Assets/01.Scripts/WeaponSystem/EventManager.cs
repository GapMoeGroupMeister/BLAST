using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    private static Dictionary<string, Action<object>> _events = new Dictionary<string, Action<object>>();

    public static void AddListener(string eventName, Action<object> callback)
    {
        //���� ���� Ű�� �־�� �ݹ��� �ִٸ�
        if (_events.TryAdd(eventName, callback) == false)
        {
            //������ ����� �ٽ� �ֱ�
            _events.Remove(eventName);
            _events.Add(eventName, callback);
        }
    }

    public static void RemoveListener(string eventName, Action<object> callback)
    {
        if(_events.ContainsKey(eventName))
            _events[eventName] -= callback;
    }

    public static void Invoke(string eventName, object parameter)
    {
        if(_events.ContainsKey(eventName))
            _events[eventName]?.Invoke(parameter);   
    }
}
