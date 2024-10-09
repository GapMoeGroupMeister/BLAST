using EasySave.Json;
using UnityEngine;
using DG.Tweening;
public class SaveManager : MonoSingleton<SaveManager>
{
    public SavePartData data;
    private readonly string fileName = "PartData";

    protected override void Awake()
    {
        base.Awake();
        data = EasyToJson.FromJson<SavePartData>(fileName);
        if(data == null)
        data = new SavePartData();
    }

    public void SaveData()
    {
        EasyToJson.ToJson<SavePartData>(data, fileName);
    }

    public void SelectPlayerPart(int id)
    {
        data.partId = id;
        SaveData();
    }

    public int GetCurrentPlayerPart() => data.partId;
}
