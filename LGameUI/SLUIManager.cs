using LUnity.Game.Resource;
using LUnity.Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****
 * 
 *  ui 界面管理类
 * 
 */

namespace LUnity.Game.UI
{

    public class SLUIManager
    {

        /// <summary>
        /// 保存当前界面的根节点
        /// </summary>
        private static GameObject mPageRoot;

        /// <summary>
        /// 保存所以的界面信息
        /// </summary>
        private static Dictionary<int, CLPageEntity> mPageInfos;

        /// <summary>
        /// 保存当前打开的界面
        /// 
        /// 默认最后一个为当前打开的界面
        /// </summary>
        private static List<CLUIPage> mOpenPages;

        /// <summary>
        /// 当前打开的界面
        /// </summary>
        private static CLUIPage mCurrentPage;

        /// <summary>
        /// 界面与界面之间的跨度
        /// </summary>
        private static int PageDepth = 30;

        /// <summary>
        /// 当前深度
        /// </summary>
        private static int mCurrentDepth = 0;

        /// <summary>
        /// 加载所有的界面信息
        /// </summary>
        public static void InitPageInfo(List<CLPageEntity> pages, GameObject root)
        {
            mPageRoot = root;
            mPageInfos = new Dictionary<int, CLPageEntity>();
            mOpenPages = new List<CLUIPage>();
            mCurrentPage = null;

            if (pages == null)
            {
                SLUnityLog.Error("游戏界面加载数据不存在!!");
                return;
            }

            for (int i = 0, len = pages.Count; i < len; i++)
            {
                CLPageEntity entity = pages[i];
                mPageInfos.Add(entity.PageId, entity);
            }
        }

        /// <summary>
        /// 验证页面是否打开
        /// </summary>
        public static int IsOpenPage(int pageId)
        {
            if (mOpenPages == null || pageId < 0) return 0;
            return mOpenPages.FindIndex(x => x.mPageId == pageId);
        }

        /// <summary>
        /// 打开某个界面
        /// </summary>
        /// <param name="pageId">界面的id</param>
        public static void OpenPage(int pageId)
        {
            CLPageEntity entity;
            if (pageId < 0 || mPageInfos == null || !mPageInfos.TryGetValue(pageId, out entity))
            {
                SLUnityLog.Error("打开的界面id不存在！！");
                return;
            }

            if (mCurrentPage != null && mCurrentPage.mPageId == pageId)
            {
                SLUnityLog.Error("该界面已经在最上层打开！！");
                return;
            }

            if (mCurrentPage != null)
            {
                mCurrentPage.OnLoseFocus();
                mCurrentDepth = mCurrentPage.mDepth + PageDepth;
            }

            int index = IsOpenPage(pageId);
            CLUIPage curPage = null;

            if (index > 0)
            {
                // 已经存在
                curPage = mOpenPages[index];
                mOpenPages.RemoveAt(index);
            }
            else
            {
                // 不存在
                GameObject pageObj = SLResourceManager.SyncLoadResource(entity.PagePath, entity.PageName);
                curPage = SLUnityUtil.Create<CLUIPage>(pageObj, mPageRoot);
                curPage.mPageId = entity.PageId;
            }

            if (curPage == null)
            {
                SLUnityLog.Error("页面创建失败！！");
                return;
            }

            curPage.SetPageDepth(mCurrentDepth);
            curPage.OnFocus();
            mOpenPages.Add(curPage);

            mCurrentPage = curPage;
        }

        /// <summary>
        /// 刷新所有的界面
        /// </summary>
        public static void OnRefresh()
        {
            if (mOpenPages == null || mOpenPages.Count <= 0) return;
            for (int i = 0, len = mOpenPages.Count; i < len; i++)
            {
                mOpenPages[i].OnRefresh();
            }
        }

        /// <summary>
        /// 刷新某个界面
        /// </summary>
        /// <param name="pageId"></param>
        public static void OnRefresh(int pageId)
        {
            if (pageId <= 0 || mOpenPages == null || mOpenPages.Count <= 0) return;
            CLUIPage page = mOpenPages.Find(x => x.mPageId == pageId);
            if (page == null) return;
            page.OnRefresh();
        }

        /// <summary>
        /// 关闭所有打开的界面
        /// </summary>
        public static void OnClosePage()
        {
            mCurrentDepth = 0;
            if (mOpenPages == null || mOpenPages.Count <= 0) return;
            for (int i = 0, len = mOpenPages.Count; i < len; i++)
            {
                mOpenPages[i].OnDestroyPage();
            }
            mOpenPages.Clear();
        }

        /// <summary>
        /// 关闭打开的界面
        /// </summary>
        public static void OnClosePage(int pageId)
        {
            if (pageId <= 0 || mOpenPages == null || mOpenPages.Count <= 0) return;
            CLUIPage page = mOpenPages.Find(x => x.mPageId == pageId);
            if (page == null) return;
            page.OnDestroyPage();
            mOpenPages.Remove(page);
        }

    }

}