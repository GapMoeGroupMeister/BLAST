#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Crogen.AttributeExtension.Editor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ButtonEditor : UnityEditor.Editor
    {
        private MonoBehaviour _monoBehaviour;

        private void OnEnable()
        {
            _monoBehaviour = target as MonoBehaviour;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MethodInfo[] methods = _monoBehaviour.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (MethodInfo method in methods)
            {
                Button buttonAttribute = Attribute.GetCustomAttribute(method, typeof(Button)) as Button;

                if (buttonAttribute != null)
                {
                    string buttonLabel = string.IsNullOrEmpty(buttonAttribute.ButtonName) ? method.Name : buttonAttribute.ButtonName;
                    GUILayout.Space(buttonAttribute.Space);
                    if (GUILayout.Button(buttonLabel))
                    {
                        method.Invoke(_monoBehaviour, null);
                    }
                }
            }
        }
    }
}
#endif
