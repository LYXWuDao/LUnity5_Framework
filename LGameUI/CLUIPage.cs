using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LUnity.Game.UI
{
    public class CLUIPage : MonoBehaviour
    {

        public CLUnityDepth[] mPageDepth;

        /// <summary>
        /// 界面的id
        /// </summary>
        public int mPageId = 0;

        /// <summary>
        /// 界面的深度
        /// </summary>
        public int mDepth = 0;

        private void Awake()
        {
            OnAwake();
        }

        /// <summary>
        /// 设置界面的深度
        /// </summary>
        /// <param name="depth"></param>
        public void SetPageDepth(int depth)
        {
            mDepth = depth;
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        public void OnClose()
        {
            SLUIManager.OnClosePage(mPageId);
        }

        /// <summary>
        /// 消耗界面
        /// </summary>
        public void OnDestroyPage()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        private void OnDestroy()
        {
            OnClear();
        }

        public virtual void OnAwake()
        {

        }

        /// <summary>
        /// 获得焦点
        /// </summary>
        public virtual void OnFocus()
        {

        }

        /// <summary>
        /// 失去焦点
        /// </summary>
        public virtual void OnLoseFocus()
        {

        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        public virtual void OnRefresh()
        {

        }

        /// <summary>
        /// 清理数据
        /// </summary>
        public virtual void OnClear()
        {

        }

    }
}
