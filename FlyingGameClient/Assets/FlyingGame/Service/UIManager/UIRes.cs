using UnityEngine;
using System.Collections;

namespace Kurisu.Service.UIManager
{
    /// <summary>
    /// UI资源
    /// </summary>
    public static class UIRes
    {
        public static string UIResRoot = "ui/";

        /// <summary>
        /// 加载UI的Prefab
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject LoadPrefab(string name)
        {
            GameObject asset = Resources.Load(UIResRoot + name) as GameObject;

            return asset;
        }
    }
}

