using Kurisu.Setting;
using System.Collections;
using SGF.Utils;
using UnityEngine;
using SGF;

namespace Kurisu
{
    /// <summary>
    /// App的配置定义
    /// </summary>
    public class AppConfig
    {
        private static AppConfigSetting setting;

        public static string Path = ConfigConstants.AppConfigPath;

        public static AppConfigSetting Setting
        {
            get
            {
                return setting;
            }
        }

        public static void Init()
        {
            Debugger.Log("AppConfig", "Init() Path = " + Path);

            setting = JsonUtils.LoadJsonFromFile<AppConfigSetting>(Path);

            if (setting == null)
            {
                setting = new AppConfigSetting();
                setting.EnableMusic = true;
                setting.EnableSoundEffect = true;
            }
        }

        public static void Save()
        {
            Debugger.Log("AppConfig", "Save() setting = " + setting);

            if (setting != null)
            {
                JsonUtils.WriteDataToJsonFile(Path, setting);
            }
        }
    }
}

