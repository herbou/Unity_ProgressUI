using System;
using EasyUI.Helpers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(CustomColor))]
public class CustomColorEditor : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float width = 50f;
        float space = 4f;
        Rect colorRect = new Rect(position.x, position.y, width, position.height);
        Rect valueRect = new Rect(position.x+width+space, position.y, position.width-width-space, position.height);

        SerializedProperty colorProp = property.FindPropertyRelative("color");
        EditorGUI.LabelField(colorRect,colorProp.enumNames[colorProp.enumValueIndex]);
        /*GUI.enabled = false;
        EditorGUI.PropertyField(colorRect, property.FindPropertyRelative("color"), GUIContent.none);
        GUI.enabled = true;*/
        EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("value"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement container = new VisualElement();

        PropertyField colorField = new PropertyField(property.FindPropertyRelative("color"));
        PropertyField valueField = new PropertyField(property.FindPropertyRelative("value"));

        container.Add(colorField);
        container.Add(valueField);

        return container;
    }
    /*
        private void OnEnable() {
            colorProp = serializedObject.FindProperty("color");
            valueProp = serializedObject.FindProperty("value");
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(colorProp, new GUIContent("...."), true);
            EditorGUILayout.PropertyField(valueProp, new GUIContent("vvvv"), true);

            serializedObject.ApplyModifiedProperties();
        }*/
}