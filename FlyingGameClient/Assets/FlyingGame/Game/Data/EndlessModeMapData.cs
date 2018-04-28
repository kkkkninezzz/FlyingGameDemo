using System;
using System.Collections.Generic;

namespace Kurisu.Game.Data
{
    /// <summary>
    /// 无尽模式下的地图信息
    /// </summary>
    [Serializable]
    public class EndlessModeMapData : ModeMapData
    {
        /// <summary>
        /// 每次生成地图时，都会从MapParts中随机
        /// </summary>
        public List<MapPartData> mapParts;
    }
}
