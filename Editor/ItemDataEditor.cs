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
        // serializedObjectは、ヒエラルキーで選択中のSerializedObject
        _Object = serializedObject.FindProperty("Object");
        _itemConditions = serializedObject.FindProperty("itemConditions");
    }

    public override void OnInspectorGUI()
    {
        // インスペクターの表示をカスタマイズしたいときはこっちで行う

        // これを呼び出すと、標準で用意されるインスペクターがそのまま表示される
        base.OnInspectorGUI();

        // 独自のインスペクター表示を行いたいときは、上記を呼び出さずに
        // 自前でビューをつくるなどする
    }

    public void OnSceneGUI()
    {
        // シーンビューでGUIを描画したいときはこっちで行う
    }
}
