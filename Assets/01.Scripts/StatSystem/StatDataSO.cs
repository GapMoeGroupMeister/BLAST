using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/StatData")]
public class StatDataSO : ScriptableObject
{
    public SerializedDictionary<StatEnum, float> statDictionary;

    [ContextMenu("Reset")]
    public void Reset()
    {
        statDictionary = new SerializedDictionary<StatEnum, float>();

        foreach (StatEnum stat in Enum.GetValues(typeof(StatEnum)))
        {
            statDictionary.Add(stat, 0f);
        }
    }

    public float GetValue(StatEnum stat)
    {
        if (statDictionary.TryGetValue(stat, out float value)) 
            return value;
        return 0f;
    }
}