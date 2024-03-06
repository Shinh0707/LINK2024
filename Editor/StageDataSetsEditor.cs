using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using File = UnityEngine.Windows.File;

[CanEditMultipleObjects]
[CustomEditor(typeof(StageDataSets))]
public class StageDataSetsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        StageDataSets obj = target as StageDataSets;
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("StageData"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("previewMode"));
        if (!string.IsNullOrEmpty(obj.StageData.GetStageName(obj.previewMode)))
        {
            string scenePath = SceneObjectEditor.GetScenePath(obj.StageData.GetStageName(obj.previewMode));
            string preview_path = Path.Combine(Path.GetDirectoryName(scenePath), Path.ChangeExtension(Path.GetFileNameWithoutExtension(scenePath) + "__preview__", Path.GetExtension(scenePath)));
            if (EditorSceneExists(Path.GetFileNameWithoutExtension(preview_path), out int searchedScene))
            {
                if (GUILayout.Button("Close Preview"))
                {
                    EditorSceneManager.CloseScene(SceneManager.GetSceneAt(searchedScene), true);
                }
            }
            else
            {
                if (GUILayout.Button("Preview"))
                {

                    if (!File.Exists(preview_path)) AssetDatabase.CopyAsset(scenePath, preview_path);
                    var preview_scene = EditorSceneManager.OpenScene(preview_path, OpenSceneMode.Additive);
                    _ = SceneManager.SetActiveScene(preview_scene);

                }

            }
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ItemDataList"));
        serializedObject.ApplyModifiedProperties();
    }

    public bool EditorSceneExists(string sceneName,out int searchedScene)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).IsValid())
            {
                searchedScene = i;
                if (string.IsNullOrEmpty(sceneName)) return true;
                if ((SceneManager.GetSceneAt(i).name == sceneName)) return true;
            }
        }
        searchedScene = -1;
        return false;
    }

    //private async Task LoadNewPreviewScene(string scenePath, string preview_path)
    //{
    //    nowloading = true;
    //    if()
    //    {
    //        while ()
    //        {

    //        }
    //    }


    //}
}
