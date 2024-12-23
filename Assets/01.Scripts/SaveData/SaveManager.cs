using EasySave.Json;
using UnityEngine;
using DG.Tweening;
public class SaveManager : MonoSingleton<SaveManager>
{
    public static SavePartData data;
    private static readonly string fileName = "PartData";

    protected override void Awake()
    {
        base.Awake();

        if (data == null)
            data = new SavePartData();
    }

    public static void SaveData()
    {
        EasyToJson.ToJson<SavePartData>(data, fileName);
    }

    public static void SelectPlayerPart(int id)
    {
        data.partId = id;
        SaveData();
    }

    public static int GetCurrentPlayerPart()
    {
        if (data == null)
        {
            data = EasyToJson.FromJson<SavePartData>(fileName);
            if(data == null)
            {
                data = new SavePartData();
            }
        }
        return data.partId;
    }
}
