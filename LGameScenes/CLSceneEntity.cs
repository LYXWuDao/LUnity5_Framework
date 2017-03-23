using System.Collections;
using System.Collections.Generic;

/****
 * 
 *     场景管理类
 * 
 */

namespace LUnity.Game.Scene
{

    public class CLSceneEntity
    {

        /// <summary>
        ///  场景id
        /// </summary>
        public int SceneId = 0;

        /// <summary>
        /// 场景名字
        /// </summary>
        public string SceneName = string.Empty;

        /// <summary>
        /// 场景路径
        /// </summary>
        public string ScenePath = string.Empty;

        /// <summary>
        /// 场景打开后加载的界面
        /// </summary>
        public int PageId = 0;
    }

}
