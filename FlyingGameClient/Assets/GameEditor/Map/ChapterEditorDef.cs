#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Kurisu.GameEditor.Map
{
    /// <summary>
    /// 关卡编辑器定义
    /// </summary>
    public static class ChapterEditorDef
    {
        /// <summary>
        /// 关卡模式配置文件的根目录
        /// </summary>
        public static string ChapterConfigRootPath = "Assets/Resources/Config/Map/ChapterMode/";

        /// <summary>
        /// 无尽模式配置文件的根目录
        /// </summary>
        public static string EndlessConfigRootPath = "Assets/Resources/Config/Map/EndlessMode/";

        public const string MapParts = "MapParts";

        public const string MapPart = "MapPart";

        public const string MapPartPrefabPath = "GameEditor/Map/MapPart";

        public const string RandomGameObjectPool = "RandomGameObjectPool";

        public const string StartPosition = "StartPosition";

        public const string EndPosition = "EndPosition";

        public const string BasicPart = "BasicPart";

        public const string DynamicGameObjects = "DynamicGameObjects";

        public const string BirthPoints = "BirthPoints";

        /// <summary>
        /// 出生点预制体的路径
        /// </summary>
        public const string BirthPointPrefabPath = "map/BirthPoint";
    }
}
#endif