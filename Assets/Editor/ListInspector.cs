using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(NPC))]
	public class ListInspector : Editor {

	public override void OnInspectorGUI () {

		serializedObject.Update();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("elementName"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("description"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("profileImage"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("guilt"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("location"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("visible"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("box"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("weaponProficiency"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("highClass"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("scene"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("trust"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("enumName"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("mouseOverIcon"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("relations"), true);
		serializedObject.ApplyModifiedProperties();

	}

}
