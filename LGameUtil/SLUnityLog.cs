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
        /// 输出日志
        /// </summary>
        public static void Log(string message)
        {
            if (SLUnityConfig.IsDebugMode)
            {
                Debug.Log("[LGame Log]: " + message);
            }

            if (SLUnityConfig.IsWriteLogToFile)
            {
                WriteLogToFile(message);
            }
        }

        public static void Warning(string message)
        {
            if (SLUnityConfig.IsDebugMode)
            {
                Debug.LogWarning("[LGame Warning]: " + message);
            }
        }

        public static void Error(string message)
        {
            if (SLUnityConfig.IsDebugMode)
            {
                Debug.LogError("[LGame Error]: " + message);
            }

            if (SLUnityConfig.IsWriteLogToFile)
            {
                WriteLogToFile(message);
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

                // 日志文件大于10 M 清理文件
                if (MyFileSize > 10)
                {
                    File.WriteAllText(path, "");
                }
            }

            File.AppendAllText(path, string.Format("[File Log]: {0}\r\n", message));
        }

    }

}
