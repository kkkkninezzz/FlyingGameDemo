#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Kurisu.GameEditor.Uitls
{
    public static class EditorUtils
    {

        /// <summary>
        /// 根据给定的GameObject对象找到对应预制体路径
        /// 
        /// 如果GameObject不存在对应的预制体则返回null
        /// </summary>
        /// <param name="gameObj"></param>
        /// <returns></returns>
        public static string FindPrefabFullPathByGameObject(GameObject gameObj)
        {
            UnityEngine.Object prefab = PrefabUtility.GetPrefabParent(gameObj);
            if (prefab == null)
            {
                return null;
            }

            string path = AssetDatabase.GetAssetPath(prefab);

            // 将".prefab"的后缀移除掉
            return path.Remove(path.Length - 7, 7);
        }

        /// <summary>
        /// 根据给定的GameObject对象找到对应预制体路径
        /// 该路径不包含 Assets/Resources/ 以及后缀名
        /// 
        /// 如果GameObject不存在对应的预制体则返回null
        /// </summary>
        /// <param name="gameObj"></param>
        /// <returns></returns>
        public static string FindPrefabPathByGameObject(GameObject gameObj)
        {
            UnityEngine.Object prefab = PrefabUtility.GetPrefabParent(gameObj);
            if (prefab == null)
            {
                return null;
            }

            string path = AssetDatabase.GetAssetPath(prefab);

            // 将".prefab"的后缀移除掉
            return path.Remove(path.Length - 7, 7).Remove(0, 17);
        }

        /// <summary>
        /// 获取资源的路径，并且该路径不包含后缀名
        /// </summary>
        /// <param name="assetObj"></param>
        /// <returns></returns>
        public static string FindAssetFullPath(UnityEngine.Object assetObj)
        {

            string path = AssetDatabase.GetAssetPath(assetObj);

            // 将后缀移除掉
            int dotIndex = path.LastIndexOf(".");

            // 不包含后缀
            if (dotIndex < 0)
            {
                return path;

            }
            return path.Remove(dotIndex);
        }

        /// <summary>
        /// 获取资源的路径，并且该路径不包含 Assets/Resources/ 以及后缀名
        /// </summary>
        /// <param name="assetObj"></param>
        /// <returns></returns>
        public static string FindAssetPath(UnityEngine.Object assetObj)
        {

            string path = AssetDatabase.GetAssetPath(assetObj);

            // 将后缀移除掉
            int dotIndex = path.LastIndexOf(".");

            // 不包含后缀
            if (dotIndex < 0)
            {
                return path;

            }

            return path.Remove(dotIndex).Remove(0, 17);
        }
    }
}
#endif

