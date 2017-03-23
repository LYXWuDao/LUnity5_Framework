using LUnity.Game.Scene;
using LUnity.Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****
 * 
 *   异步加载场景
 * 
 */

namespace LUnity.Game.Resource
{


    public class CLAsyncLoadScene : MonoBehaviour
    {

        private AsyncOperation AsyncAsset;

        private CLResourceEntity AsyncEntity;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// 开始加载场景
        /// </summary>
        /// <returns></returns>
        public IEnumerator StartLoadScene(AsyncOperation request, CLResourceEntity entity)
        {
            if (request == null)
            {
                SLUnityLog.Error("异步加载 AsyncOperation 不存在!, request = null");
                yield return 0;
            }

            AsyncAsset = request;
            AsyncEntity = entity;

            yield return request;

            if (entity == null)
            {
                SLUnityLog.Error("异步加载 场景接受数据为空!, entity = null");
                yield return 0;
            }

            entity.IsAysncLoad = true;
            entity.LoadProgress = 1;
            entity.IsDone = true;
            if (entity.Finish != null) entity.Finish(entity);
            Destroy(gameObject);
        }

        private void Update()
        {
            if (AsyncAsset == null || AsyncEntity == null) return;

            if (AsyncAsset.isDone)
            {
                AsyncEntity.LoadProgress = 1;
                AsyncEntity.IsDone = true;
                return;
            }

            if (AsyncAsset.progress < 0.9f)
            {
                AsyncEntity.LoadProgress = AsyncAsset.progress;
            }
            else
            {
                AsyncEntity.LoadProgress = 1;
            }

        }

    }

}
