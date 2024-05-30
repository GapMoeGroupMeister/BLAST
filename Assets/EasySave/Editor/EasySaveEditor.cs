using UnityEditor;
using UnityEngine;
using EasySave.Json;
using EasySave.Xml;

public class EasySaveEditor : EditorWindow
{
    private string _easyJsonPath;
    private string _easyXmlPath;
    
    private static GUIStyle LabelStyle => new GUIStyle(GUI.skin.label)
    {
        fontSize = 20,
        fontStyle = FontStyle.Bold,
        alignment = TextAnchor.MiddleCenter
    };
    
    [MenuItem("EasySave/PathEditor")]
    public static void ShowWindow()
    {
        EasySaveEditor window = GetWindow<EasySaveEditor>("PathEditor");
        window.minSize = new Vector2(400, 150);
        window.Show();
    }
    
    private void OnGUI()
    {
        GUILayout.Label("EasySave Path Editor", LabelStyle);
        
        EditorGUILayout.Space(10f);

        #region TextFields
        EditorGUILayout.BeginHorizontal("HelpBox");
        {
            GUILayout.Label(new GUIContent("Json Path", "Json 파일이 저장될 경로"));
            _easyJsonPath = EditorGUILayout.TextField(_easyJsonPath);
            
            GUILayout.Label(new GUIContent("Xml Path", "Xml 파일이 저장될 경로"));
            _easyXmlPath = EditorGUILayout.TextField(_easyXmlPath);
            
        }
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Buttons
        EditorGUILayout.BeginHorizontal("HelpBox");
        {
            if (GUILayout.Button(new GUIContent("Save Json Path", "Json 파일 경로를 적용")))
            {
                if (IsPathValid(_easyJsonPath))
                {
                    EasyToJson.LocalPath = Application.dataPath + _easyJsonPath;
                    Debug.Log($"저장 경로: {EasyToJson.LocalPath}");
                }
            }
            
            if (GUILayout.Button(new GUIContent("Save Xml Path", "Xml 파일 경로를 적용")))
            {
                if (IsPathValid(_easyXmlPath))
                {
                    EasyToXml.LocalPath = Application.dataPath + _easyXmlPath;
                    Debug.Log($"저장 경로: {EasyToXml.LocalPath}");
                }
            }
        }
        EditorGUILayout.EndHorizontal();
        #endregion

        EditorGUILayout.Space(10f);
        if (GUILayout.Button(new GUIContent("Create Folder", "Json, Xml 폴더를 생성")))
        {
            EasyToJson.CreateJsonFolder();
            EasyToXml.CreateXmlFolder();
        }
    }
    
    private bool IsPathValid(string path)
    {
        // 경로가 비어있는지 확인
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("경로가 비어있습니다.");
            return false;
        }
        
        // "/"로 시작하고 "/"로 끝나는지 확인
        if (path[0] != '/' || path[^1] != '/')
        {
            Debug.LogError("경로는 '/'로 시작하고 '/'로 끝나야 합니다.");
            return false;
        }

        return true;
    }
}
