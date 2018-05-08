using System;
using System.Collections.Generic;


namespace Kurisu.Game.Data
{
    /// <summary>
    /// 用于存储地图数据的类
    /// </summary>
    [Serializable]
    public abstract class MapData
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string no;

        /// <summary>
        /// 名称
        /// </summary>
        public string name;

        /// <summary>
        /// 地图模式
        /// </summary>
        public MapMode mapMode;

        /// <summary>
        /// 背景音乐路径
        /// </summary>
        public List<string> bgmPaths;

        /// <summary>
        /// 天空盒路径
        /// </summary>
        public string skyboxPath;

        /// <summary>
        /// 出生点信息
        /// </summary>
        public List<TransformData> birthPoints;

        public MapData(MapMode mapMode)
        {
            this.mapMode = mapMode;
        }
    }
}
