using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****
 * 
 *      游戏配置类
 * 
 */

namespace LUnity.Game.Util
{

    public class SLUnityConfig : MonoBehaviour
    {

        /// <summary>
        /// 是否是 debug 模式
        /// </summary>
        public static bool IsDebugMode { get; set; }

        /// <summary>
        /// 是否写日志到文件
        /// </summary>
        public static bool IsWriteLogToFile { get; set; }

        /// <summary>
        /// 写日志到窗口上
        /// </summary>
        public static bool IsWriteLogToWindow { get; set; }

        public bool isDebug = true;

        public bool isToFile = true;

        public bool isToWindow = true;

        void Awake()
        {
            IsDebugMode = isDebug;
            IsWriteLogToFile = isToFile;
            IsWriteLogToWindow = isToWindow;
        }

    }

}