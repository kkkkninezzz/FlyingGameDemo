﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kurisu.Game.Data
{
    /// <summary>
    /// 游戏对象数据
    /// </summary>
    [Serializable]
    public class GameObjectData
    {
        /// <summary>
        /// 游戏对象的资源路径
        /// </summary>
        public string Path;

        /// <summary>
        /// 这个游戏对象实例化的概率
        /// </summary>
        public float Probability;

        /// <summary>
        /// transform数据
        /// </summary>
        public TransformData TransformData;
    }
}