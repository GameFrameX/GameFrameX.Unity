using System.Collections.Generic;
using UnityEditor;

namespace GameFrameX.Editor
{
    public abstract class ComponentTypeComponentInspector : GameFrameworkInspector
    {
        protected SerializedProperty ComponentType = null;
        protected string[] ComponentTypeNames = null;
        protected int ComponentTypeNameIndex = 0;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                int componentTypeSelectedIndex = EditorGUILayout.Popup("ComponentType", ComponentTypeNameIndex, ComponentTypeNames);
                if (componentTypeSelectedIndex != ComponentTypeNameIndex)
                {
                    ComponentTypeNameIndex = componentTypeSelectedIndex;
                    ComponentType.stringValue = componentTypeSelectedIndex <= 0 ? null : ComponentTypeNames[componentTypeSelectedIndex];
                }
            }

            serializedObject.ApplyModifiedProperties();

            Repaint();
        }

        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();

            RefreshTypeNames();
        }

        protected virtual void Enable()
        {
        }

        private void OnEnable()
        {
            ComponentType = serializedObject.FindProperty("m_ComponentType");
            Enable();
            RefreshTypeNames();
        }

        protected abstract void RefreshTypeNames();

        protected void RefreshComponentTypeNames(System.Type type)
        {
            List<string> managerTypeNames = new List<string>
            {
                NoneOptionName
            };

            managerTypeNames.AddRange(Type.GetRuntimeTypeNames(type));
            ComponentTypeNames = managerTypeNames.ToArray();
            ComponentTypeNameIndex = 0;
            if (!ComponentType.stringValue.IsNullOrEmpty())
            {
                ComponentTypeNameIndex = managerTypeNames.IndexOf(ComponentType.stringValue);
                if (ComponentTypeNameIndex <= 0)
                {
                    ComponentTypeNameIndex = 0;
                    ComponentType.stringValue = null;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}