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
    [Header("基本設定")]
    [Tooltip("ステージに対応するシーン")]
    public SceneObject StageScene;
    [Header("大人プレイヤー設定")]
    public CharacterStageData Man;
    [Header("子供プレイヤー設定")]
    public CharacterStageData Child;
}

[Serializable]
public class ItemCondition
{
    [Serializable]
    [Flags]
    public enum Set
    {
        [Description("配置自由")]
        Free = 0,
        [Description("配置必須")]
        Must = 1
    }
    [Tooltip("ステージの配置方法")]
    public Set SetMode = Set.Free;
    [Serializable]
    [Flags]
    public enum Visible
    {
        None = 0,
        Man = 1,
        Child = 2,
        Both = 3
    }
    [Tooltip("誰に見えるか")]
    public Visible VisibleMode = Visible.Both;
    public Vector3 Position;
    public Vector3 Rotation;
}

[Serializable]
public class ItemRange : MonoBehaviour
{
    [Tooltip("出現の個数範囲を個別に決めるか")]
    public bool MinMaxSetSeparate = false;
    public ItemSetRange InMan;
    public ItemSetRange InChild;
}

[Serializable]
public class ItemSetRange
{
    [Tooltip("アイテムの最小出現個数")]
    public int Min = 0;
    [Tooltip("アイテムの最大出現個数")]
    public int Max = 1;
    public bool modified = false;

    public ItemSetRange(int Min,int Max)
    {
        this.Min = Min;
        this.Max = Max;
    }
    public ItemSetRange(ItemSetRange itemSetRange)
    {
        Min = itemSetRange.Min;
        Max = itemSetRange.Max;
    }
}
[Serializable]
public class ItemData : MonoBehaviour
{
    [Tooltip("アイテムのプレファブ")]
    public GameObject Object;
    [Header("出現設定")]
    public ItemRange ItemRange;
    [Tooltip("アイテムの出現情報")]
    public List<ItemCondition> itemConditions = new();
}

[CreateAssetMenu(menuName = "DataSets/Stage Data", fileName = "StageDataSets")]
public class StageDataSets : ScriptableObject
{
    public StageData StageData;
    public List<ItemData> ItemDataList;
}