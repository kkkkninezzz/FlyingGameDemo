using UnityEngine;
using Kurisu.Game.Data;

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

    }
}
