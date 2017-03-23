using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*****
 * 
 *  窗口日志输出
 *  
 *  单例, 游戏中只有一个window 日志输出类
 * 
 */

namespace LUnity.Game.Util
{
    public class CLWindowLog : MonoBehaviour
    {

        /// <summary>
        /// 窗口日志显示类型
        /// </summary>
        public enum WinLogType
        {
            /// <summary>
            /// 无类型
            /// </summary>
            None,

            /// <summary>
            /// 日志
            /// </summary>
            Log,

            /// <summary>
            /// 警告
            /// </summary>
            Warning,

            /// <summary>
            /// 错误
            /// </summary>
            Error,
        }

        /// <summary>
        /// 窗口日志实体
        /// </summary>
        public class WinLogEntity
        {
            /// <summary>
            /// 消息
            /// </summary>
            public string Message = string.Empty;

            /// <summary>
            /// 消息类型
            /// </summary>
            public WinLogType lgType = WinLogType.None;

            /// <summary>
            /// 消息显示颜色
            /// </summary>
            public Color LogColor = Color.white;
        }

        /// <summary>
        ///  当前窗口日志实例
        /// </summary>
        private static CLWindowLog _windowLog = null;

        /// <summary>
        /// 当前window 日志类型
        /// </summary>
        private WinLogType mLogType = WinLogType.None;

        /// <summary>
        /// 最大显示条数
        /// </summary>
        private int MaxWinCount = 100;

        /// <summary>
        /// 所有消息列表
        /// </summary>
        private List<WinLogEntity> mAllList = new List<WinLogEntity>();

        /// <summary>
        /// 当前显示的显示列表
        /// </summary>
        private List<WinLogEntity> mCurrList = new List<WinLogEntity>();

        /// <summary>
        /// 得到窗口日志实例
        /// 
        /// 如果实例为空就创建
        /// </summary>
        public static CLWindowLog Instance
        {
            get
            {
                if (_windowLog == null)
                {
                    _windowLog = SLUnityUtil.Create<CLWindowLog>("_window log");
                }
                return _windowLog;
            }
        }

        /// <summary>
        /// 输出window 日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="lType">日志类型</param>
        public void WriteWinLog(string message, LogType lType)
        {

            if (mAllList.Count > MaxWinCount)
            {
                mAllList.RemoveAt(0);
            }

            WinLogType ltype = WinLogType.None;
            Color color = Color.white;
            switch (lType)
            {
                case LogType.Log:
                    ltype = WinLogType.Log;
                    break;
                case LogType.Warning:
                    ltype = WinLogType.Warning;
                    color = Color.yellow;
                    break;
                case LogType.Error:
                case LogType.Exception:
                    ltype = WinLogType.Error;
                    color = Color.red;
                    break;
            }

            mAllList.Add(new WinLogEntity()
            {
                Message = message,
                lgType = ltype,
                LogColor = color
            });
        }

        void Awake()
        {
            mAllList.Clear();
            mCurrList.Clear();
        }

        /// <summary>
        /// 得到当前显示的列表
        /// </summary>
        public void InitShowList()
        {
            if (mLogType == WinLogType.None)
            {
                mCurrList = mAllList;
            }
            else
            {
                mCurrList = mAllList.FindAll(x => x.lgType == mLogType);
            }
        }

        /// <summary>
        /// 显示日志
        /// </summary>
        void OnGUI()
        {
            InitShowList();
            GUILayout.Window(0, new Rect(10, 10, Screen.width - 20, Screen.height - 20), DrawConsoleWindow, "window log");

        }

        /// <summary>
        /// 当前文字滚动条位置
        /// </summary>
        private Vector2 scrollPos = Vector2.zero;

        /// <summary>
        /// 显示日志内容
        /// </summary>
        /// <param name="id"></param>
        private void DrawConsoleWindow(int id)
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("所有"))
            {
                mLogType = WinLogType.None;
            }

            if (GUILayout.Button("日志(Log)"))
            {
                mLogType = WinLogType.Log;
            }

            if (GUILayout.Button("警告(Warning)"))
            {
                mLogType = WinLogType.Warning;
            }

            if (GUILayout.Button("错误(Error)"))
            {
                mLogType = WinLogType.Error;
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginArea(new Rect(0, 50, Screen.width * 0.95f, (Screen.height - 50) * 0.95f));

            scrollPos = GUILayout.BeginScrollView(scrollPos);
            for (int i = 0, len = mCurrList.Count; i < len; i++)
            {
                WinLogEntity entity = mCurrList[i];
                GUI.skin.label.normal.textColor = entity.LogColor;
                GUILayout.Label(entity.Message);
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        void OnDestroy()
        {
            mAllList.Clear();
            mCurrList.Clear();
            _windowLog = null;
        }
    }
}
