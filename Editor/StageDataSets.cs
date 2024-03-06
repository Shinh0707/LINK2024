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
    [Header("��{�ݒ�")]
    [Tooltip("�X�e�[�W�ɑΉ�����V�[��")]
    public SceneObject StageScene;
    [Header("��l�v���C���[�ݒ�")]
    public CharacterStageData Man;
    [Header("�q���v���C���[�ݒ�")]
    public CharacterStageData Child;
}

[Serializable]
public class ItemCondition
{
    [Serializable]
    [Flags]
    public enum Set
    {
        [Description("�z�u���R")]
        Free = 0,
        [Description("�z�u�K�{")]
        Must = 1
    }
    [Tooltip("�X�e�[�W�̔z�u���@")]
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
    [Tooltip("�N�Ɍ����邩")]
    public Visible VisibleMode = Visible.Both;
    public Vector3 Position;
    public Vector3 Rotation;
}

[Serializable]
public class ItemRange : MonoBehaviour
{
    [Tooltip("�o���̌��͈͂��ʂɌ��߂邩")]
    public bool MinMaxSetSeparate = false;
    public ItemSetRange InMan;
    public ItemSetRange InChild;
}

[Serializable]
public class ItemSetRange
{
    [Tooltip("�A�C�e���̍ŏ��o����")]
    public int Min = 0;
    [Tooltip("�A�C�e���̍ő�o����")]
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
    [Tooltip("�A�C�e���̃v���t�@�u")]
    public GameObject Object;
    [Header("�o���ݒ�")]
    public ItemRange ItemRange;
    [Tooltip("�A�C�e���̏o�����")]
    public List<ItemCondition> itemConditions = new();
}

[CreateAssetMenu(menuName = "DataSets/Stage Data", fileName = "StageDataSets")]
public class StageDataSets : ScriptableObject
{
    public StageData StageData;
    public List<ItemData> ItemDataList;
}