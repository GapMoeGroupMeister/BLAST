using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyStatData")]
public class EnemyStatDataSO : StatDataSO
{
    public SerializedDictionary<StatEnum, AnimationCurve> statModifierDictionary;

    [ContextMenu("Reset")]
    public override void Reset()
    {
        base.Reset();
        statModifierDictionary = new SerializedDictionary<StatEnum, AnimationCurve>();

        foreach (StatEnum stat in Enum.GetValues(typeof(StatEnum)))
        {
            statModifierDictionary.Add(stat, new AnimationCurve());
        }
    }

    public float GetModifierValue(StatEnum stat, float time)
    {
        if (statModifierDictionary.TryGetValue(stat, out AnimationCurve curve))
            return curve.Evaluate(time);
        return 0f;
    }
}
