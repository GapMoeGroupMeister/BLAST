using EasySave.Json;

public class SaveManager : MonoSingleton<SaveManager>
{
    public SavePartData data;
    private readonly string fileName = "PartData";

    private void Start()
    {
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
}
