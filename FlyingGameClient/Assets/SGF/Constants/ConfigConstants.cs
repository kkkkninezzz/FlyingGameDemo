using UnityEngine;
using Kurisu.Game.Data;
using Kurisu.Setting.UserSetting;
using System.Collections.Generic;

namespace SGF
{
    public static class ConfigConstants
    {
        public const string ENABLE_LOG_CONDITION = "EnableLog";

        public const string DEFAULT_DOMAIN = "Kurisu.Module";

        public const string DEFAULT_PREFAB_PATH = "default/DefaultPrefab";

        /// <summary>
        /// 相机的默认位置
        /// </summary>
        public static readonly Vector3 DEFAULT_CAMERA_POSITION = new Vector3(0, 1, -10);

        /// <summary>
        /// 相机的默认转向
        /// </summary>
        public static readonly Quaternion DEFAULT_CAMERA_ROTATION = new Quaternion();

        #region 获取常量的字符串描述
        public static string GetDescription(GameMode mode)
        {
            switch (mode)
            {
                case GameMode.EndlessPVE:
                    return "无尽模式";
                case GameMode.NormalPVE:
                    return "正常模式";
                case GameMode.TimelimitPVE:
                    return "限时模式";
                default:
                    return "未知的模式";
            }
        }

        public static string GetDescription(MapMode mode)
        {
            switch (mode)
            {
                case MapMode.EndlessMode:
                    return "无尽模式";
                case MapMode.NormalMode:
                    return "正常模式";
                default:
                    return "未知的模式";
            }
        }
        #endregion

        #region 配置信息路径
        public static readonly string BaseFilePath =
#if UNITY_ANDROID
                                                    Application.persistentDataPath + "/";
#else
                                                    Application.streamingAssetsPath + "/";
#endif

        public static readonly string LogFileDir = BaseFilePath + "Log/DebuggerLog/";

        public static readonly string AppConfigPath = BaseFilePath + "Config/AppConfig.json";

        public static readonly string UnlockedChapterDataPath = BaseFilePath + "Config/User/UnlockedChapterData.json";

        //public static readonly string MapSettingPath = BaseFilePath + "Config/Map/MapSetting.json";

        /// <summary>
        /// 地图配置的资源路径
        /// </summary>
        public static readonly string MapSettingPath = "Config/Map/MapSetting";


        #endregion

        #region 默认值区域
        static ConfigConstants()
        {
            // 初始化UnlockedChapterData
            DefaultUnlockedChapterData = new UnlockedChapterData();
            InitUnlockedChapterData();


        }
        public static readonly UnlockedChapterData DefaultUnlockedChapterData;

        private static void InitUnlockedChapterData()
        {
            int firstChapterNo = 1;
            string firstSmallChapterNo = "1_1";

            List<string> chapters = new List<string>();
            chapters.Add(firstSmallChapterNo);
            KeyValuePair<int, List<string>> kv = new KeyValuePair<int, List<string>>(firstChapterNo, chapters);

            DefaultUnlockedChapterData.unlockedChapters = new List<KeyValuePair<int, List<string>>>();
            DefaultUnlockedChapterData.unlockedChapters.Add(kv);
        }

        #endregion
    }
}
