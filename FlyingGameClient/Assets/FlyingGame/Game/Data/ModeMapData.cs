using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kurisu.Game.Data
{
    /// <summary>
    /// 用于存储地图数据的类
    /// </summary>
    [Serializable]
    public class ModeMapData
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
