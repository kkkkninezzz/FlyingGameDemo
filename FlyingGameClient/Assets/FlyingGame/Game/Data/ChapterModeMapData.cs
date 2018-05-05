using Kurisu.GameEditor.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kurisu.Game.Data
{
    /// <summary>
    /// 关卡模式下的地图数据
    /// </summary>
    public class ChapterModeMapData : ModeMapData
    {
        /// <summary>
        /// 地图数据
        /// </summary>
        public MapPartData mapPart;

        public ChapterModeMapData() : base(MapMode.ChapterMode)
        {

        }
    }
}
