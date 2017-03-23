using LUnity.Game.Resource;
using LUnity.Game.UI;
using LUnity.Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/****
 * 
 *     场景管理类
 * 
 */

namespace LUnity.Game.Scene
{

    public class SLScenesManager
    {

        /// <summary>
        /// 保存所以场景信息
        /// </summary>
        private static Dictionary<int, CLSceneEntity> mSceneInfos;

        /// <summary>
        /// 当前场景
        /// </summary>
        private static CLScenePage mCurrScene;

        /// <summary>
        /// 初始化场景信息
        /// </summary>
        public static void InitSceneInit(List<CLSceneEntity> infos)
        {
            mSceneInfos = new Dictionary<int, CLSceneEntity>();
            mCurrScene = null;

            if (infos == null)
            {
                SLUnityLog.Error("游戏加载场景数据不存在!!");
                return;
            }

            for (int i = 0, len = infos.Count; i < len; i++)
            {
                CLSceneEntity entity = infos[i];
                mSceneInfos.Add(entity.SceneId, entity);
            }

        }

        /// <summary>
        /// 打开场景
        /// </summary>
        /// <param name="sceId">场景的id</param>
        public static void OpenScene(int sceId)
        {
            if (sceId <= 0)
            {
                SLUnityLog.Error("你准备打开的场景id不合法!!");
                return;
            }

            CLSceneEntity entity;
            if (mSceneInfos == null || mSceneInfos.Count <= 0 || !mSceneInfos.TryGetValue(sceId, out entity))
            {
                SLUnityLog.Error("你准备打开的场景数据不存在!!");
                return;
            }

            if (mCurrScene != null && mCurrScene.mSceneId == sceId)
            {
                SLUnityLog.Error("该场景已经打开!!");
                return;
            }

            if (mCurrScene != null)
            {
                mCurrScene.OnLeaveScene();
            }

            SLUIManager.OnClosePage();

            SLResourceManager.AsyncLoadScene(new CLResourceEntity()
            {
                ResourcePath = entity.ScenePath,
                ResourceName = entity.SceneName,
                LoadProgress = 0,
                IsAysncLoad = true,
                Finish = delegate (CLResourceEntity resEntity)
                {
                    CLScenePage scene = SLUnityUtil.Find<CLScenePage>("SceneRoot");

                    if (scene == null)
                    {
                        SLUnityLog.Error("场景打开失败！！");
                        return;
                    }

                    scene.mSceneId = entity.SceneId;
                    scene.mPageId = entity.PageId;
                    scene.OnEnterScene();
                    mCurrScene = scene;
                    // 场景打开后打开界面
                    SLUIManager.OpenPage(entity.PageId);
                }
            });

        }

    }

}
