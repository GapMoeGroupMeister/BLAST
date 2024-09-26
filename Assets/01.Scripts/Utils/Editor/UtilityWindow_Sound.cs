using System.IO;
using SoundManage;
using UnityEditor;
using UnityEngine;
using AudioType = SoundManage.AudioType;

public partial class UtilityWindow
{
    private readonly string _soundDirectory = "Assets/08.SO/Sound";
    private SoundTableSO _soundTable;
    
    private void InputSoundTable()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField("Sound Table", EditorStyles.boldLabel);
            _soundTable = (SoundTableSO) EditorGUILayout.ObjectField(_soundTable, typeof(SoundTableSO), false);
        }
        EditorGUILayout.EndHorizontal();
    }
    
    private void DrawSoundItems()
    {
        InputSoundTable();
        
        if (_soundTable == null)
            return;
        
        CreateSoundSODraw();

        #region Sound List
    
        GUI.color = Color.white;
        EditorGUILayout.BeginHorizontal();
        {
            #region Scroll View

            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300f));
            {
                EditorGUILayout.LabelField("Sound List", EditorStyles.boldLabel);
                EditorGUILayout.Space(3f);
                
                scrollPositions[UtilType.Sound] = EditorGUILayout.BeginScrollView(
                    scrollPositions[UtilType.Sound], false, true,
                    GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
                {
                    DrawSoundTable();
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();

            #endregion
            
            DrawSoundInspector();
        }
        EditorGUILayout.EndHorizontal();

        #endregion
    }

    private void DrawSoundInspector()
    {
        if (selectedItem[UtilType.Sound] != null)
        {
            Vector2 scroll = Vector2.zero;
            EditorGUILayout.BeginScrollView(scroll);
            {
                EditorGUILayout.Space(2f);
                Editor.CreateCachedEditor(
                    selectedItem[UtilType.Sound], null, ref _cachedEditor);

                _cachedEditor.OnInspectorGUI();
            }
            EditorGUILayout.EndScrollView();
        }
    }

    private void DrawSoundTable()
    {
        foreach (var soundSO in _soundTable.soundSOList)
        {
            GUIStyle style = selectedItem[UtilType.Sound] == soundSO
                ? _selectStyle
                : GUIStyle.none;
            
            EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
            {
                EditorGUILayout.LabelField(soundSO.name, GUILayout.Width(240f), GUILayout.Height(40f));
                
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.Space(10f);
                    SoundDeleteButton(soundSO);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
            if (soundSO == null)
                break;
            
            GetRect(soundSO);
        }
    }

    private void SoundDeleteButton(SoundSO soundSo)
    {
        GUI.color = Color.red;
        if (GUILayout.Button("X", GUILayout.Width(20f)))
        {
            Debug.Log("삭제");
            _soundTable.soundSOList.Remove(soundSo);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(soundSo));
            EditorUtility.SetDirty(_soundTable);
            AssetDatabase.SaveAssets();
        }
        GUI.color = Color.white;
    }

    private void GetRect(SoundSO soundSo)
    {
        Rect rect = GUILayoutUtility.GetLastRect();
        
        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            inspectorScroll = Vector2.zero;
            selectedItem[UtilType.Sound] = soundSo;
            Event.current.Use();
        }
    }

    private void CreateSoundSODraw()
    {
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Create Sound SO"))
            {
                CreateSoundSO();
            }
        }
        EditorGUILayout.EndHorizontal();
    }
    
    private void CreateSoundSO()
    {
        SoundSO soundSO = CreateInstance<SoundSO>();
        soundSO.name = "New Sound";
        if (_soundTable.audioType == AudioType.BGM)
        {
            soundSO.audioType = AudioType.BGM;
        }
        else
        {
            soundSO.audioType = AudioType.SFX;
        }
        string path = $"{_soundDirectory}/{_soundTable.name}";
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
        AssetDatabase.CreateAsset(soundSO, $"{path}/Sound_{soundSO.soundName}.asset");
        _soundTable.soundSOList.Add(soundSO);
        EditorUtility.SetDirty(_soundTable);
        AssetDatabase.SaveAssets();
    }
}