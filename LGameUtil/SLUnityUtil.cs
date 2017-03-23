using LUnity.Game.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/****
 * 
 * 
 * 游戏公用工具类
 * 
 * 
 */

namespace LUnity.Game.Util
{
    public class SLUnityUtil
    {

        /// <summary>
        /// 
        /// 启动游戏, 使用这个框架都需要调用
        /// 
        /// 完成配置
        /// 
        /// </summary>
        public static void LaunchGame()
        {
            // 初始化日志堆栈
            SLUnityLog.ListenerLogStack();

            // 加载所有资源依赖
            SLResourceManager.LoadAssetBundleManifest();

            // 加载配置文件
            GameObject config = SLResourceManager.SyncLoadResource("config.data", "config");
            SLUnityConfig slConfig = Create<SLUnityConfig>(config);

        }

        /// <summary>
        /// 创建一个节点
        /// </summary>
        /// <param name="name">节点名字</param>
        /// <returns></returns>
        public static GameObject Create(string name)
        {
            return InitTransform(new GameObject(name));
        }

        /// <summary>
        /// 创建一个节点
        /// </summary>
        /// <param name="name">节点名字</param>
        /// <param name="parent">父节点</param>
        /// <returns></returns>
        public static GameObject Create(string name, GameObject parent)
        {
            if (parent == null) return Create(name);
            return Create(name, parent.transform);
        }

        /// <summary>
        /// 创建一个节点
        /// </summary>
        /// <param name="name">节点名字</param>
        /// <param name="parent">父节点</param>
        /// <returns></returns>
        public static GameObject Create(string name, Transform parent)
        {
            GameObject go = Create(name);
            if (parent == null) return go;
            go.transform.parent = parent;
            InitTransform(go);
            return go;
        }

        /// <summary>
        /// 创建一个节点
        /// </summary>
        /// <typeparam name="T">节点上挂接脚本类型</typeparam>
        /// <param name="name">节点名字</param>
        /// <returns></returns>
        public static T Create<T>(string name) where T : Component
        {
            GameObject go = Create(name);
            if (go == null) return null;
            return GetComponent<T>(go);
        }

        /// <summary>
        /// 创建一个节点
        /// </summary>
        /// <typeparam name="T">节点上挂接脚本类型</typeparam>
        /// <param name="name">节点名字</param>
        /// <param name="parent">父节点</param>
        /// <returns></returns>
        public static T Create<T>(string name, GameObject parent) where T : Component
        {
            if (parent == null) return Create<T>(name);
            return Create<T>(name, parent.transform);
        }

        /// <summary>
        /// 创建一个节点
        /// </summary>
        /// <typeparam name="T">节点上挂接脚本类型</typeparam>
        /// <param name="name">节点名字</param>
        /// <param name="parent">父节点</param>
        /// <returns></returns>
        public static T Create<T>(string name, Transform parent) where T : Component
        {
            T go = Create<T>(name);
            if (parent == null) return go;
            go.transform.parent = parent;
            InitTransform(go.gameObject);
            return go;
        }

        /// <summary>
        /// 实例化导入的资源
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        public static GameObject Create(GameObject go)
        {
            if (go == null) return null;
            return InitTransform(GameObject.Instantiate(go));
        }

        /// <summary>
        /// 实例化导入的资源
        /// </summary>
        /// <param name="go"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static GameObject Create(GameObject go, GameObject parent)
        {
            if (go == null) return null;
            if (parent == null) return InitTransform(GameObject.Instantiate(go));
            return Create(go, parent.transform);
        }

        /// <summary>
        /// 实例化导入的资源
        /// </summary>
        /// <param name="go"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static GameObject Create(GameObject go, Transform parent)
        {
            if (go == null) return null;
            if (parent == null) return InitTransform(GameObject.Instantiate(go));
            return InitTransform(GameObject.Instantiate(go, parent));
        }

        /// <summary>
        /// 实例化导入的资源
        /// </summary>
        /// <typeparam name="T">预设上挂载的脚本</typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static T Create<T>(GameObject go) where T : Component
        {
            GameObject create = Create(go);
            if (create == null) return default(T);
            return GetComponent<T>(create);
        }

        /// <summary>
        /// 实例化导入的资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T Create<T>(GameObject go, GameObject parent) where T : Component
        {
            GameObject create = Create(go, parent);
            if (create == null) return default(T);
            return GetComponent<T>(create);
        }

        /// <summary>
        /// 实例化导入的资源
        /// </summary>
        /// <param name="go"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T Create<T>(GameObject go, Transform parent) where T : Component
        {
            GameObject create = Create(go, parent);
            if (create == null) return default(T);
            return GetComponent<T>(create);
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject Find(string nodePath)
        {
            if (string.IsNullOrEmpty(nodePath)) return null;
            return GameObject.Find(nodePath);
        }

        /// <summary>
        /// 在根节点下面查找
        /// </summary>
        /// <param name="root"></param>
        /// <param name="nodePath"></param>
        /// <returns></returns>
        public static GameObject Find(GameObject root, string nodePath)
        {
            if (string.IsNullOrEmpty(nodePath)) return null;
            if (root == null) return Find(nodePath);
            return Find(root.transform, nodePath);
        }

        /// <summary>
        /// 在根节点下面查找
        /// </summary>
        /// <param name="root"></param>
        /// <param name="nodePath"></param>
        /// <returns></returns>
        public static GameObject Find(Transform root, string nodePath)
        {
            if (string.IsNullOrEmpty(nodePath)) return null;
            if (root == null) return Find(nodePath);
            Transform trans = root.Find(nodePath);
            return trans == null ? null : trans.gameObject;
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Find<T>(string nodePath) where T : Component
        {
            if (string.IsNullOrEmpty(nodePath)) return default(T);
            GameObject node = GameObject.Find(nodePath);
            return GetComponent<T>(node);
        }

        /// <summary>
        /// 在根节点下面查找
        /// </summary>
        /// <param name="root"></param>
        /// <param name="nodePath"></param>
        /// <returns></returns>
        public static T Find<T>(GameObject root, string nodePath) where T : Component
        {
            if (string.IsNullOrEmpty(nodePath)) return default(T);
            if (root == null) return Find<T>(nodePath);
            return Find<T>(root.transform, nodePath);
        }

        /// <summary>
        /// 在根节点下面查找
        /// </summary>
        /// <param name="root"></param>
        /// <param name="nodePath"></param>
        /// <returns></returns>
        public static T Find<T>(Transform root, string nodePath) where T : Component
        {
            if (string.IsNullOrEmpty(nodePath)) return default(T);
            if (root == null) return Find<T>(nodePath);
            Transform trans = root.Find(nodePath);
            if (trans == null) return default(T);
            return GetComponent<T>(trans.gameObject);
        }

        /// <summary>
        /// 得到预设上面的脚本
        /// 
        /// 如果没有脚本就增加一个脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static T GetComponent<T>(GameObject go) where T : Component
        {
            if (go == null) return default(T);
            T comp = go.GetComponent<T>();
            if (comp == null) comp = go.AddComponent<T>();
            return comp;
        }

        /// <summary>
        /// 初始化节点的位置
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        public static GameObject InitTransform(GameObject go)
        {
            if (go == null) return go;
            Transform trans = InitTransform(go.transform);
            return go;
        }

        /// <summary>
        /// 初始化节点的位置
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static Transform InitTransform(Transform trans)
        {
            if (trans == null) return trans;
            trans.localPosition = Vector3.zero;
            trans.localScale = Vector3.one;
            trans.localRotation = Quaternion.identity;
            return trans;
        }
    }
}
