using System.Collections;
using System;
using System.Collections.Generic;

namespace Kurisu.Game.Data
{
    [Serializable]
    public class MapData
    {
        /// <summary>
        /// 地图id，通过id找到地图资源
        /// </summary>
        public int id = 0;

        /// <summary>
        /// 地图的名称，可以用来显示
        /// </summary>
        public string name = "";

    }
}
