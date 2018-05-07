using UnityEngine;
using System.Collections.Generic;
using Kurisu.Service.Core;
using Kurisu.Setting;
using SGF.Utils;

namespace Kurisu.Module.Map
{   
    /// <summary>
    /// 地图模块
    /// </summary>
    public class MapModule : ServiceModule<MapModule>
    {
        public static string MapSettingPath = "Assets/Resources/Config/Map/MapSetting.json";

        /// <summary>
        /// 地图的配置信息
        /// </summary>
        private MapSettingData MapSetting;

        private MapModule()
        {

        }

        public void Init()
        {
            MapSetting = JsonUtils.LoadJsonFromFile<MapSettingData>(MapSettingPath);

            if (MapSetting == null)
            {
                MapSetting = new MapSettingData();
                MapSetting.chapterModeConfigs = new List<List<MapConfigData>>(0);
                MapSetting.endlessModeConfigs = new List<MapConfigData>(0);
            }
        }

        
    }
}

