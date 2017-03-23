using LUnity.Game.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****
 * 
 *   场景内容基类
 * 
 */

namespace LUnity.Game.Scene
{


    public class CLScenePage : MonoBehaviour
    {

        /// <summary>
        /// 场景id 
        /// </summary>
        public int mSceneId = 0;

        /// <summary>
        /// 进入创建后打开的界面
        /// </summary>
        public int mPageId = 0;

        private void Awake()
        {
            OnAwake();
        }

        public virtual void OnAwake()
        {

        }

        /// <summary>
        /// 进入场景
        /// </summary>
        public virtual void OnEnterScene()
        {

        }

        /// <summary>
        /// 离开场景
        /// </summary>
        public virtual void OnLeaveScene()
        {

        }

    }

}
