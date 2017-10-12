using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateUIStatechartAssets
{
    [MenuItem("Assets/Create/UI Statechart")]
    public static void CreateUIStatechart()
    {
        UIStatechart asset = ScriptableObject.CreateInstance<UIStatechart>();

        asset.Name = "New UI Statechart";
        AssetDatabase.CreateAsset(asset, "Assets/"+asset.Name+".asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
