using System;
using System.Collections.Generic;
using Kurisu.Game.Data;
using Kurisu.GameEditor.Map;

namespace Kurisu.Setting
{
    [Serializable]
    public class MapConfigData
    {
        public string no;

        public string name;

        public GameMode gameMode;

        public MapMode mapMode;

        public string configPath;
    }

    [Serializable]
    public class ChapterMapConfigData
    {
        /// <summary>
        /// 章节编号
        /// </summary>
        public int chapterNo;

        /// <summary>
        /// 该章节下的地图配置
        /// </summary>
        public List<MapConfigData> chapterConfigs;
    }

    [Serializable]
    public class MapSettingData
    {
        /// <summary>
        /// 关卡模式地图配置
        /// </summary>
        public List<ChapterMapConfigData> chapterModeConfigs;

        /// <summary>
        /// 无尽模式地图配置
        /// </summary>
        public List<MapConfigData> endlessModeConfigs;
    }
}

