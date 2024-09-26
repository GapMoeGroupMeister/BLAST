using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public enum UtilType
{
    Sound
}

public partial class UtilityWindow : EditorWindow
{
    private static int toolbarIndex = 0;
    private static Dictionary<UtilType, Vector2> scrollPositions 
        = new Dictionary<UtilType, Vector2>();
    private static Dictionary<UtilType, Object> selectedItem 
        = new Dictionary<UtilType, Object>();
    
    private static Vector2 inspectorScroll = Vector2.zero;

    private string[] _toolbarItemNames;
    private Editor _cachedEditor;
    private Texture2D _selectTexture;
    private GUIStyle _selectStyle;
    
    [MenuItem("Util/UtilManager")]
    private static void OpenWindow()
    {
        UtilityWindow window = GetWindow<UtilityWindow>("UtilManager");
        window.minSize = new Vector2(700, 500);
        window.Show();
    }

    private void OnEnable()
    {
        SetUpUtility();
    }

    private void CreateDirectory(string directoryPath)
    {
        if (Directory.Exists(directoryPath) == false)
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    private void OnDisable()
    {
        DestroyImmediate(_cachedEditor);
        DestroyImmediate(_selectTexture);
    }

    private void SetUpUtility()
    {
        foreach (UtilType type in Enum.GetValues(typeof(UtilType)))
        {
            if (scrollPositions.ContainsKey(type) == false)
                scrollPositions[type] = Vector2.zero;
            if (selectedItem.ContainsKey(type) == false)
                selectedItem[type] = null;
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    
    private T CreateAssetTable<T>(string path) where T: ScriptableObject
    {
        T table =  AssetDatabase.LoadAssetAtPath<T>($"{path}/table.asset");
        if(table == null)
        {
            table = ScriptableObject.CreateInstance<T>();
            
            string fileName = AssetDatabase.GenerateUniqueAssetPath($"{path}/table.asset");
            AssetDatabase.CreateAsset(table, fileName);
            Debug.Log($"{typeof(T).Name} Table Created At : {fileName}");
            
        }
        return table;
    }

    private void OnGUI()
    {
        DrawContent(toolbarIndex);
    }

    private void DrawContent(int toolbarIndex)
    {
        DrawSoundItems();
    }
}
