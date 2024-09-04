using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class TechTreeGenerator : EditorWindow
{
    private TechTreeGraphView graphView;
    private InspectorView inspectorView;

    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Tools/TechTreeGenerator")]
    public static void ShowExample()
    {
        TechTreeGenerator wnd = GetWindow<TechTreeGenerator>();
        wnd.titleContent = new GUIContent("TechTreeGenerator");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        if (Selection.activeObject is TechTreeSO)
        {
            ShowExample();
            return true;
        }
        return false;
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        m_VisualTreeAsset.CloneTree(root);

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/01.Scripts/UI/SkillTree/Editor/TechTreeGenerator.uss");
        root.styleSheets.Add(styleSheet);

        graphView = root.Q<TechTreeGraphView>();
        inspectorView = root.Q<InspectorView>();

        graphView.OnNodeSelected = OnNodeSelectionChanged;

        OnSelectionChange();
    }

    private void OnSelectionChange()
    {
        TechTreeSO dialog = Selection.activeObject as TechTreeSO;
        if (dialog)
            graphView.ParpurateView(dialog);
    }

    private void OnNodeSelectionChanged(NodeView view)
    {
        inspectorView.UpdateSelection(view);
    }
}
