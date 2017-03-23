using UnityEngine;
using UnityEditor;
using System.IO;
using LUnity.Game.Util;

public class LSResEditor
{

    [MenuItem("Assets/资源打包")]
    public static void CreateAssetbundle()
    {
        Caching.CleanCache();

        BuildPipeline.BuildAssetBundles(SLUnityAddress.GameResourcePath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

        AssetDatabase.Refresh();

        Debug.Log("AssetBundle打包完毕");

    }

}
