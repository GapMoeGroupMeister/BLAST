using EasySave.Json;
public static class SaveManager
{
    public static SavePartData data;
    private static readonly string fileName = "PartData";


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
            if (data == null)
            {
                data = new SavePartData();
            }
        }
        return data.partId;
    }
}
