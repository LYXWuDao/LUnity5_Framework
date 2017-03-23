using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

/******
 * 
 * 
 *  日志输出类  
 * 
 */

namespace LUnity.Game.Util
{
    public class SLUnityLog
    {

        /// <summary>
        /// 日志堆栈回调
        /// </summary>
        /// <param name="logString"></param>
        /// <param name="stackTrace"></param>
        /// <param name="type"></param>
        private static void UnityLogStackCallback(string logString, string stackTrace, LogType type)
        {
            if (SLUnityConfig.IsWriteLogToFile)
            {
                WriteLogToFile(logString + "\n" + stackTrace);
            }

            if (SLUnityConfig.IsWriteLogToWindow)
            {
                // WriteLogToWindow(logString + "\n" + stackTrace, type);
            }
        }

        /// <summary>
        /// 
        /// 监听日志输出堆栈
        /// 
        /// </summary>
        public static void ListenerLogStack()
        {
            Application.logMessageReceived += UnityLogStackCallback;
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        public static void Log(string message)
        {
            if (SLUnityConfig.IsDebugMode)
            {
                Debug.Log("[LGame Log]: " + message);
            }
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            if (SLUnityConfig.IsDebugMode)
            {
                Debug.LogWarning("[LGame Warning]: " + message);
            }
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            if (SLUnityConfig.IsDebugMode)
            {
                Debug.LogError("[LGame Error]: " + message);
            }
        }

        /// <summary>
        /// 将日志输出到文件
        /// </summary>
        public static void WriteLogToFile(string message)
        {
            string path = SLUnityAddress.GameLogPath;

            if (File.Exists(path))
            {
                FileInfo finfo = new FileInfo(path);
                float MyFileSize = (float)finfo.Length / (1024 * 1024);

                // 日志文件大于10M 清理文件
                if (MyFileSize > 10)
                {
                    File.WriteAllText(path, "");
                }
            }

            File.AppendAllText(path, string.Format("[File Log]: {0}\r\n", message));
        }

        /// <summary>
        /// 将日志输出到窗口上
        /// </summary>
        /// <param name="message"></param>\
        /// <param name="ltype"></param>
        public static void WriteLogToWindow(string message, LogType ltype)
        {
            CLWindowLog.Instance.WriteWinLog(message, ltype);
        }

    }

}
