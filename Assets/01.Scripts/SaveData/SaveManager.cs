using EasySave.Json;

public class SaveManager : MonoSingleton<SaveManager>
{
    private SavePartData _data;
    private readonly string fileName = "PartData";

    private void Start()
    {
        _data = EasyToJson.FromJson<SavePartData>(fileName);
    }

    public void SaveData(SavePartData data)
    {
        EasyToJson.ToJson<SavePartData>(data, fileName);
    }
}
