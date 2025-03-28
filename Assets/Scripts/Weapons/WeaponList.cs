using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponInventory))]
public class WeaponList : Editor
{
    SerializedProperty weaponList;

    private void OnEnable()
    {
        weaponList = serializedObject.FindProperty("weapons");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        weaponList.isExpanded = EditorGUILayout.Foldout(weaponList.isExpanded, "Weapons");

        if (weaponList.isExpanded)
        {
            EditorGUI.indentLevel++;

            for (int i = 0; i < weaponList.arraySize; i++)
            {
                var weapon = weaponList.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(weapon, new GUIContent(((WeaponType)i).ToString()));
            }

            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
