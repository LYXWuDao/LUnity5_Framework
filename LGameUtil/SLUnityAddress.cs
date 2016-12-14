using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/****
 * 
 *      游戏地址管理
 *      
 *      资源路径，登录地址 等
 * 
 */

namespace LUnity.Game.Util
{

    public class SLUnityAddress
    {

        /// <summary>
        /// 游戏服务器地址
        /// </summary>
        public static string GameServerList
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// 游戏资源下载地址
        /// </summary>
        public static string GameCDNUrl
        {

            get
            {
                return "";
            }
        }

        /// <summary>
        /// 游戏聊天地址
        /// </summary>
        public static string GameChatUrl
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// 游戏充值地址
        /// </summary>
        public static string GamePayUrl
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// 游戏资源存放路径
        /// 
        /// 分 ios, pc, Android
        /// 
        /// 默认返回 pc 端路径
        /// </summary>
        public static string GameResourcePath
        {
            get
            {
                string resPath = Application.dataPath + "/LGRP/Assets";
                if (!Directory.Exists(resPath)) Directory.CreateDirectory(resPath);
                return resPath;
            }
        }

        /// <summary>
        /// 游戏 ui 资源加载路径
        /// </summary>
        public static string GameUIPath
        {
            get
            {
                return GameResourcePath + "/ui";
            }
        }

        /// <summary>
        /// 游戏 场景 资源加载路径
        /// </summary>
        public static string GameScenesPath
        {
            get
            {
                return GameResourcePath + "/scenes";
            }
        }

        /// <summary>
        /// 游戏 声音 资源加载路径
        /// </summary>
        public static string GameSoundPath
        {
            get
            {
                return GameResourcePath + "/sound";
            }
        }

        /// <summary>
        /// 游戏 贴图 资源加载路径
        /// </summary>
        public static string GameTexturePath
        {
            get
            {
                return GameResourcePath + "/texture";
            }
        }

        /// <summary>
        /// 游戏 人物模型 资源加载路径
        /// </summary>
        public static string GameModelPath
        {
            get
            {
                return GameResourcePath + "/model";
            }
        }

        /// <summary>
        /// 游戏 特效 资源加载路径
        /// </summary>
        public static string GameEffectPath
        {
            get
            {
                return GameResourcePath + "/effect";
            }
        }

        /// <summary>
        /// 游戏日志文件保存地址
        /// </summary>
        public static string GameLogPath
        {
            get
            {
                return GameResourcePath + "/file.log";
            }
        }

    }

}