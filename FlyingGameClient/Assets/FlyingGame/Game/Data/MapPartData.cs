using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kurisu.Game.Data
{
    /// <summary>
    /// 地图的部分信息
    /// </summary>
    [Serializable]
    public class MapPartData
    {
        

        /// <summary>
        /// 动态加载的游戏对象信息
        /// </summary>
        public List<GameObjectData> GameObjects;
    }
}
