#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Crogen.AttributeExtension.Editor
{
    [CustomPropertyDrawer(typeof(HideInInspectorByCondition))]
    public class HideInInspectorByConditionEditorEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            HideInInspectorByCondition hideInInspectorByCondition = attribute as HideInInspectorByCondition;

            SerializedProperty boolProperty = property.serializedObject.FindProperty(hideInInspectorByCondition.BooleanPropertyName);

            if (boolProperty == null)
            {
                EditorGUI.PropertyField(position, property);
                return;
            }

            if (hideInInspectorByCondition.Reversed)
            {
                if (boolProperty.boolValue == false)
                {
                    EditorGUI.PropertyField(position, property);
                } 
            }
            else
            {
                if (boolProperty.boolValue)
                {
                    EditorGUI.PropertyField(position, property);
                }    
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            HideInInspectorByCondition hideInInspectorByCondition = attribute as HideInInspectorByCondition;
            SerializedProperty boolProperty = property.serializedObject.FindProperty(hideInInspectorByCondition.BooleanPropertyName);
      
            if(!boolProperty.boolValue)
                return 0f;

            return EditorGUI.GetPropertyHeight(property);        }
    }
}
#endif