using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/StageInfo/InfoList")]
public class StageInfoListSO : ScriptableObject
{
    public List<StageInfoSO> list;


    public StageInfoSO GetInfo(int id)
    {
        foreach (StageInfoSO info in list)
        {
            if (info.id == id)
            {
                return info;
            }
        }
        return null;
    }

    public StageInfoSO GetInfo(string name)
    {
        foreach (StageInfoSO info in list)
        {
            if (info.stageName == name)
            {
                return info;
            }
        }
        return null;
    }
}
