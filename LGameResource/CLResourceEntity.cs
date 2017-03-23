using System;
using System.Collections;
using System.Collections.Generic;

/****
 * 
 *     场景管理类
 * 
 */

namespace LUnity.Game.Resource
{

    public class CLResourceEntity
    {

        /// <summary>
        ///  场景id
        /// </summary>
        public int ResourceId = 0;

        /// <summary>
        /// 场景名字
        /// </summary>
        public string ResourceName = string.Empty;

        /// <summary>
        /// 场景路径
        /// </summary>
        public string ResourcePath = string.Empty;

        /// <summary>
        /// 是否加载完成
        /// </summary>
        public bool IsDone = false;

        /// <summary>
        /// 是否异步加载
        /// </summary>
        public bool IsAysncLoad = false;

        /// <summary>
        /// 加载进度
        /// </summary>
        public float LoadProgress = 0f;

        /// <summary>
        /// 加载完成回调
        /// </summary>
        public Action<CLResourceEntity> Finish;
    }

}
