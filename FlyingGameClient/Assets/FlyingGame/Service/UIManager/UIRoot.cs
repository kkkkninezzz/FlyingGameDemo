using UnityEngine;
using System.Collections;

namespace Kurisu.Service.UIManager
{
    public class UIRoot : MonoBehaviour
    {
        public const string LOG_TAG = "UIRoot";

        /// <summary>
        /// 从UIRoot下通过类型寻找一个组件对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Find<T>() where T : MonoBehaviour
        {
            string name = typeof(T).Name;
            GameObject obj = Find(name);
            if (obj != null)
                return obj.GetComponent<T>();

            return null;
        }

        /// <summary>
        /// 在UIRoot下通过name & type 寻找一个组件对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Find<T>(string name) where T : MonoBehaviour
        {
            GameObject obj = Find(name);
            if (obj != null)
                return obj.GetComponent<T>();

            return null;
        }

        /// <summary>
        /// 在UIRoot下通过名字寻找一个GameObject对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject Find(string name)
        {
            Transform obj = null;
            GameObject root = FindUIRoot();

            if (root != null)
            {
                obj = root.transform.Find(name);
            }

            return obj == null ? null : obj.gameObject;
        }

        /// <summary>
        /// 在当前场景中寻找UIRoot对象
        /// </summary>
        /// <returns></returns>
        public static GameObject FindUIRoot()
        {
            GameObject root = GameObject.Find(LOG_TAG);

            if (root == null || root.GetComponent<UIRoot>() == null)
                Debugger.LogError(LOG_TAG, "FindUIRoot() UIRoot Is Not Exist!");

            return root;
        }

        /// <summary>
        /// 当将一个UIPanel添加到UIRoot下面
        /// </summary>
        /// <param name="child"></param>
        public static void AddChild(UIPanel child)
        {
            GameObject root;
            if (child == null || (root = FindUIRoot()) == null)
                return;

            child.transform.SetParent(root.transform, false);
            
        }
    }
}

