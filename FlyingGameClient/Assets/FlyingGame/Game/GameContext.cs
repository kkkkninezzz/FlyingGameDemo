using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kurisu.Game.Data;
using SGF;

namespace Kurisu.Game
{
    /// <summary>
    /// 单局游戏的上下文
    /// 用来保存单局游戏中所有逻辑都关心的数据
    /// </summary>
    public class GameContext
    {

        /// <summary>
        /// 游戏的启动参数
        /// </summary>
        public GameParam param = null;

        /// <summary>
        /// 随机数生成器
        /// </summary>
        public SGFRandom random = new SGFRandom();

        /// <summary>
        /// 当前是第几帧
        /// </summary>
        public int curFrameIndex = 0;


    }
}

