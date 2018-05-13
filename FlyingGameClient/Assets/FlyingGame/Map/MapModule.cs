using UnityEngine;
using System.Collections.Generic;
using Kurisu.Service.Core;
using Kurisu.Setting;
using SGF.Utils;
using Kurisu.Game.Data;
using SGF;

namespace Kurisu.Module.Map
{   
    /// <summary>
    /// 地图模块
    /// </summary>
    public class MapModule : ServiceModule<MapModule>
    {
        public const string MapSettingPath = "Assets/Resources/Config/Map/MapSetting.json";

        /// <summary>
        /// 地图的配置信息
        /// </summary>
        private MapSettingData m_mapSetting;

        

        private MapModule()
        {

        }

        public void Init()
        {
            InitMapSetting();
        }

        private void InitMapSetting()
        {

            this.Log("Init() Path = " + MapSettingPath);
            m_mapSetting = JsonUtils.LoadJsonFromFile<MapSettingData>(MapSettingPath);

            if (m_mapSetting == null)
            {
                this.LogWarning("Don't exists MapSetting in Path = {0}", MapSettingPath);
                m_mapSetting = new MapSettingData();
                m_mapSetting.chapterModeConfigs = new List<ChapterMapConfigData>(0);
                m_mapSetting.endlessModeConfigs = new List<MapConfigData>(0);
            }
        }

        /// <summary>
        /// 获取章节模式的地图配置信息
        /// </summary>
        /// <returns></returns>
        public List<ChapterMapConfigData> GetChapterModeConfigs()
        {
            return m_mapSetting.chapterModeConfigs;
        }

        /// <summary>
        /// 获取无尽模式的地图配置信息
        /// </summary>
        /// <returns></returns>
        public List<MapConfigData> GetEndlessModeConfigs()
        {
            return m_mapSetting.endlessModeConfigs;
        }

        /// <summary>
        /// 通过编号获取地图配置文件
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public MapData GetChapterModeMapData(int chapterNo, string no)
        {
            if (string.IsNullOrEmpty(no))
            {
                return null;
            }
            
            MapConfigData configData = null;

            foreach (ChapterMapConfigData data in m_mapSetting.chapterModeConfigs)
            {
                if (data.chapterNo == chapterNo)
                {
                    foreach (MapConfigData config in data.chapterConfigs)
                    {
                        if (no.Equals(config.no))
                        {
                            configData = config;
                            break;
                        }
                    }
                    break;
                }
            }

            if (configData == null)
            {
                this.LogError("没有找到 ChapterNo = {0} No = {1} 的配置文件", chapterNo, no);
                return null;
            }
            
            return LoadModeMapData(configData);
        }

        /// <summary>
        /// 获取无尽模式的地图
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public EndlessModeMapData GetEndlessModeMapData(string no)
        {
            if (string.IsNullOrEmpty(no))
            {
                return null;
            }

            MapConfigData configData = m_mapSetting.endlessModeConfigs.Find((MapConfigData data) => data.no.Equals(no));
            if (configData == null)
            {
                this.LogError("没有找到 No = {1} 的配置文件", no);
                return null;
            }


            return LoadModeMapData(configData) as EndlessModeMapData;
        }

        /// <summary>
        /// 通过MapConfigData加载MapData
        /// </summary>
        /// <param name="configData"></param>
        /// <returns></returns>
        public MapData LoadModeMapData(MapConfigData configData)
        {
            MapData data = null;
            switch (configData.mapMode)
            {
                case MapMode.NormalMode:
                    data = JsonUtils.LoadJsonFromFile<NormalModeMapData>(configData.configPath);
                    break;
                case MapMode.EndlessMode:
                    data = JsonUtils.LoadJsonFromFile<EndlessModeMapData>(configData.configPath);
                    break;
            }
            if (data == null)
            {
                this.LogError("加载配置文件失败, Path = {0}", configData.configPath);
            }

            return data;
        }
    }
}

