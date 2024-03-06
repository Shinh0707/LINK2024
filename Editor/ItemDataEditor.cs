using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    private SerializedProperty _Object;
    private SerializedProperty _itemConditions;

    private void OnEnable()
    {
        // serializedObject�́A�q�G�����L�[�őI�𒆂�SerializedObject
        _Object = serializedObject.FindProperty("Object");
        _itemConditions = serializedObject.FindProperty("itemConditions");
    }

    public override void OnInspectorGUI()
    {
        // �C���X�y�N�^�[�̕\�����J�X�^�}�C�Y�������Ƃ��͂������ōs��

        // ������Ăяo���ƁA�W���ŗp�ӂ����C���X�y�N�^�[�����̂܂ܕ\�������
        base.OnInspectorGUI();

        // �Ǝ��̃C���X�y�N�^�[�\�����s�������Ƃ��́A��L���Ăяo������
        // ���O�Ńr���[������Ȃǂ���
    }

    public void OnSceneGUI()
    {
        // �V�[���r���[��GUI��`�悵�����Ƃ��͂������ōs��
    }
}
