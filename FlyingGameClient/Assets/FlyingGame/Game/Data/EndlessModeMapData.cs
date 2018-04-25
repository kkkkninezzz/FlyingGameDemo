using System;
using System.Collections.Generic;

namespace Kurisu.Game.Data
{
    /// <summary>
    /// 无尽模式下的地图信息
    /// </summary>
    [Serializable]
    public class EndlessModeMapData
    {
        /// <summary>
        /// 背景音乐路径
        /// </summary>
        public List<string> BgmPaths;

        /// <summary>
        /// 天空盒路径
        /// </summary>
        public string SkyboxPath;

        /// <summary>
        /// 出生点信息
        /// </summary>
        public List<TransformData> BirthPoints;
    }
}
