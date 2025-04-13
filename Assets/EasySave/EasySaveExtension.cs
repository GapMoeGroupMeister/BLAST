using System.Collections.Generic;
using UnityEngine;
using EasySave.Json;

public static class EasySaveExtension
{
    public static void ToJson<T> (this T obj, string jsonFileName, bool prettyPrint = false)
    {
        EasyToJson.ToJson(obj, jsonFileName, prettyPrint);
    }
    
    public static T FromJson<T> (this string jsonFileName)
    {
        return EasyToJson.FromJson<T>(jsonFileName);
    }
    
    public static void ListToJson<T> (this List<T> obj, string jsonFileName, bool prettyPrint = false)
    {
        EasyToJson.ListToJson(obj, jsonFileName, prettyPrint);
    }
    
    public static List<T> FromJsonList<T> (this string jsonFileName)
    {
        return EasyToJson.ListFromJson<T>(jsonFileName);
    }

    public static void DictionaryToJson<T, TU>(this Dictionary<T, TU> obj, string jsonFileName,
        bool prettyPrint = false)
    {
        EasyToJson.DictionaryToJson(obj, jsonFileName, prettyPrint);
    }
    
    public static Dictionary<T, TU> FromJsonDictionary<T, TU>(this string jsonFileName)
    {
        return EasyToJson.DictionaryFromJson<T, TU>(jsonFileName);
    }
}
