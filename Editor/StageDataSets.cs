using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public class CharacterStageData
{
    [Tooltip("�����X�|�[���n�_")]
    public Vector3 StartPosition;
}

[Serializable]
public class StageData
{
    [Serializable]
    public enum StageMode
    {
        None,Man,Child
    }
    [Header("��l�v���C���[�ݒ�")]
    public SceneObject ManStageScene;
    public CharacterStageData Man;
    [Header("�q���v���C���[�ݒ�")]
    public SceneObject ChildStageScene;
    public CharacterStageData Child;

    public string GetStageName(StageMode stageMode)
    {
        if (stageMode == StageMode.Child) return ChildStageScene;
        if (stageMode == StageMode.Man) return ManStageScene;
        return null;
    }
}

[Serializable]
public class LocalTransformPreview
{
    public LocalTransform m_localTransform;
    public bool preview = false;
}

[Serializable]
public class ItemData
{
    [Tooltip("�A�C�e���̃v���t�@�u")]
    public GameObject Object;
    public bool Preview = false;
    [Tooltip("��l����Transform�̌��")]
    public List<LocalTransformPreview> MansTransforms;
    [Tooltip("�q������Transform�̌��")]
    public List<LocalTransformPreview> ChildsTransforms;
}

[CreateAssetMenu(menuName = "DataSets/Stage Data", fileName = "StageDataSets")]
public class StageDataSets : ScriptableObject
{
    public StageData StageData;
    public StageData.StageMode previewMode= StageData.StageMode.None;

    public List<ItemData> ItemDataList;
}