using System.Collections;
using System.Collections.Generic;

/****
 * 
 *      游戏配置类
 * 
 */

namespace LUnity.Game.Util
{

    public class SLUnityConfig
    {

        /// <summary>
        /// 是否是 debug 模式
        /// </summary>
        public static bool IsDebugMode
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 是否写日志到文件
        /// </summary>
        public static bool IsWriteLogToFile
        {
            get
            {
                return true;
            }

        }
        
    }

}