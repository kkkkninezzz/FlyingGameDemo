using UnityEngine;
using UnityEditor;

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
    }
}
