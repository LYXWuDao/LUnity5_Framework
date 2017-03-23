using LUnity.Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/****
 * 
 *     资源管理类
 * 
 */

namespace LUnity.Game.Resource
{

    public class SLResourceManager
    {

        /// <summary>
        ///  所有资源依赖关系
        /// </summary>
        private static AssetBundleManifest mAssbunfest;

        /// <summary>
        /// 游戏启动时加载
        /// 
        /// 加载所有的资源依赖
        /// </summary>
        public static void LoadAssetBundleManifest()
        {
            // 依赖地址
            string realPath = SLUnityAddress.GameResourcePath + "/Assets";

            SLUnityLog.Log("load manifest path = " + realPath);

            AssetBundle bundle = AssetBundle.LoadFromFile(realPath);

            SLUnityLog.Log("load manifest bundle = " + bundle);

            if (bundle == null) return;

            mAssbunfest = (AssetBundleManifest)bundle.LoadAsset("AssetBundleManifest");
            bundle.Unload(false);
        }

        /// <summary>
        ///  同步导入资源
        /// </summary>
        /// <param name="assetPath">资源路径(资源相对路径)</param>
        /// <param name="assetName">资源名字(资源原始名字)</param>
        /// <returns></returns>
        private static AssetBundle SyncLoadAssetBundle(string assetPath, string assetName, out AssetBundle[] dependAsset)
        {
            SLUnityLog.Log("load resource path = " + assetPath);
            SLUnityLog.Log("load resource name = " + assetName);
            dependAsset = null;

            if (mAssbunfest == null || string.IsNullOrEmpty(assetName)) return null;
            // 得到当前资源的所有依赖
            string[] addDep = mAssbunfest.GetDirectDependencies(assetPath);
            AssetBundle[] dependsAssetbundle = new AssetBundle[addDep.Length];

            for (int i = 0, len = addDep.Length; i < len; i++)
            {
                string depPath = SLUnityAddress.GameResourcePath + "/" + addDep[i];

                SLUnityLog.Log("load resource depPath = " + depPath);

                dependsAssetbundle[i] = AssetBundle.LoadFromFile(depPath);
            }

            string realPath = SLUnityAddress.GameResourcePath + "/" + assetPath;
            AssetBundle resAssbun = AssetBundle.LoadFromFile(realPath);

            dependAsset = dependsAssetbundle;
            return resAssbun;
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="resPath">资源相对路径</param>
        /// <param name="resName">资源名字（不含后缀）</param>
        /// <returns></returns>
        public static GameObject SyncLoadResource(string resPath, string resName)
        {
            AssetBundle[] dependAsset;
            AssetBundle resAssbun = SyncLoadAssetBundle(resPath, resName, out dependAsset);

            if (resAssbun == null)
            {
                SLUnityLog.Log(" 资源加载出错!!");
                return null;
            }

            GameObject ass = resAssbun.LoadAsset(resName) as GameObject;
            resAssbun.Unload(false);

            if (dependAsset != null)
            {
                for (int i = 0, len = dependAsset.Length; i < len; i++)
                {
                    dependAsset[i].Unload(false);
                }
            }

            return ass != null ? ass : null;
        }

        /// <summary>
        /// 同步加载场景
        /// </summary>
        /// <param name="assetPath"></param>
        /// <param name="assetName"></param>
        public static void SyncLoadScene(string scenePath, string sceneName)
        {
            AssetBundle[] dependAsset;
            AssetBundle resAssbun = SyncLoadAssetBundle(scenePath, sceneName, out dependAsset);

            if (resAssbun == null)
            {
                SLUnityLog.Log(" 资源加载出错!!");
                return;
            }

            SceneManager.LoadScene(sceneName);

            resAssbun.Unload(false);

            if (dependAsset != null)
            {
                for (int i = 0, len = dependAsset.Length; i < len; i++)
                {
                    dependAsset[i].Unload(false);
                }
            }

        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="entity">异步加载需要实体信息</param>
        public static void AsyncLoadScene(CLResourceEntity entity)
        {
            AssetBundle[] dependAsset;
            AssetBundle resAssbun = SyncLoadAssetBundle(entity.ResourcePath, entity.ResourceName, out dependAsset);

            if (resAssbun == null)
            {
                SLUnityLog.Log(" 资源加载出错!!");
                return;
            }

            AsyncOperation async = SceneManager.LoadSceneAsync(entity.ResourceName);

            resAssbun.Unload(false);

            if (dependAsset != null)
            {
                for (int i = 0, len = dependAsset.Length; i < len; i++)
                {
                    dependAsset[i].Unload(false);
                }
            }

            if (async == null)
            {
                SLUnityLog.Log(" 场景加载失败!!");
                return;
            }

            CLAsyncLoadScene clasync = SLUnityUtil.Create<CLAsyncLoadScene>("_async scene");
            if (clasync != null)
            {
                clasync.StartCoroutine(clasync.StartLoadScene(async, entity));
            }

        }

    }

}
