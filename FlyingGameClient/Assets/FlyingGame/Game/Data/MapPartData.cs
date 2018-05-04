using System;
using System.Collections.Generic;

using UnityEngine;

namespace Kurisu.Game.Data
{
    /// <summary>
    /// 地图的部分信息
    /// </summary>
    [Serializable]
    public class MapPartData
    {
        /// <summary>
        /// 起始位置
        /// </summary>
        public Vector3Data startPosition;

        /// <summary>
        /// 结束位置
        /// </summary>
        public Vector3Data endPosition;

        /// <summary>
        /// 该地图部分的基础预制体对象
        /// </summary>
        public GameObjectData basicPart;

        /// <summary>
        /// 随机产生的游戏对象，每次会从列表中随机取一个
        /// </summary>
        public List<List<GameObjectData>> randomGameObjectPool;

        /// <summary>
        /// 动态加载的游戏对象信息
        /// </summary>
        public List<RandomGameObjectData> dynamicGameObjects;
    }
}
