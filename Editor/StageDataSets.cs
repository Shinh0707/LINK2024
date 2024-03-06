using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public class CharacterStageData
{
    [Tooltip("初期スポーン地点")]
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
    [Header("大人プレイヤー設定")]
    public SceneObject ManStageScene;
    public CharacterStageData Man;
    [Header("子供プレイヤー設定")]
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
    [Tooltip("アイテムのプレファブ")]
    public GameObject Object;
    public bool Preview = false;
    [Tooltip("大人側のTransformの候補")]
    public List<LocalTransformPreview> MansTransforms;
    [Tooltip("子供側のTransformの候補")]
    public List<LocalTransformPreview> ChildsTransforms;
}

[CreateAssetMenu(menuName = "DataSets/Stage Data", fileName = "StageDataSets")]
public class StageDataSets : ScriptableObject
{
    public StageData StageData;
    public StageData.StageMode previewMode= StageData.StageMode.None;

    public List<ItemData> ItemDataList;
}