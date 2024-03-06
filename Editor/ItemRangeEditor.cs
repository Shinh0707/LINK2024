using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(ItemRange))]
public class ItemRangeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawProperties();
        serializedObject.ApplyModifiedProperties();
    }

    //各要素の描画
    void DrawProperties()
    {
        ItemRange obj = target as ItemRange;
        EditorGUILayout.LabelField("個数設定", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        obj.MinMaxSetSeparate = EditorGUILayout.Toggle("個数範囲を個別に定める", obj.MinMaxSetSeparate);
        if (obj.MinMaxSetSeparate) {
            EditorGUILayout.LabelField("大人");
            EditorGUI.indentLevel++;
        }
        obj.InMan.Min = Mathf.Clamp(EditorGUILayout.IntField("最小出現個数", obj.InMan.Min), 0, 20);
        obj.InMan.Max = Mathf.Clamp(EditorGUILayout.IntField("最大出現個数", obj.InMan.Max), obj.InMan.Min, 20);
        if (obj.MinMaxSetSeparate)
        {
            EditorGUI.indentLevel--;
            EditorGUILayout.LabelField("子供");
            EditorGUI.indentLevel++;
            obj.InChild.Min = Mathf.Clamp(EditorGUILayout.IntField("最小出現個数", obj.InChild.Min), 0, 20);
            obj.InChild.Max = Mathf.Clamp(EditorGUILayout.IntField("最大出現個数", obj.InChild.Max), obj.InChild.Min, 20);
            obj.InChild.modified = true;
            EditorGUI.indentLevel--;
        }
        else if(!obj.InChild.modified)
        {
            obj.InChild = new ItemSetRange(obj.InMan);
        }
        EditorGUI.indentLevel--;

        EditorUtility.SetDirty(target);
    }
}