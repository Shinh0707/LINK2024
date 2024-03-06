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

    //�e�v�f�̕`��
    void DrawProperties()
    {
        ItemRange obj = target as ItemRange;
        EditorGUILayout.LabelField("���ݒ�", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        obj.MinMaxSetSeparate = EditorGUILayout.Toggle("���͈͂��ʂɒ�߂�", obj.MinMaxSetSeparate);
        if (obj.MinMaxSetSeparate) {
            EditorGUILayout.LabelField("��l");
            EditorGUI.indentLevel++;
        }
        obj.InMan.Min = Mathf.Clamp(EditorGUILayout.IntField("�ŏ��o����", obj.InMan.Min), 0, 20);
        obj.InMan.Max = Mathf.Clamp(EditorGUILayout.IntField("�ő�o����", obj.InMan.Max), obj.InMan.Min, 20);
        if (obj.MinMaxSetSeparate)
        {
            EditorGUI.indentLevel--;
            EditorGUILayout.LabelField("�q��");
            EditorGUI.indentLevel++;
            obj.InChild.Min = Mathf.Clamp(EditorGUILayout.IntField("�ŏ��o����", obj.InChild.Min), 0, 20);
            obj.InChild.Max = Mathf.Clamp(EditorGUILayout.IntField("�ő�o����", obj.InChild.Max), obj.InChild.Min, 20);
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