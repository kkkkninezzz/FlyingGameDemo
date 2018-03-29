using UnityEngine;
using System;

namespace SGF.Utils
{
    public static class GameObjectUtils
    {
        public static void SetActiveRecursively(GameObject go, bool value)
        {
            go.SetActive(value);
        }

        /// <summary>
        /// 获取target上的目标组件，如果target上不存在该组件，则添加目标组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T EnsureComponent<T>(GameObject target) where T : Component
        {
            T comp = target.GetComponent<T>();

            if (comp == null)
            {
                return target.AddComponent<T>();
            }

            return comp;
        }

        /// <summary>
        /// 获取target上的目标组件，如果target上不存在该组件，则添加目标组件
        /// </summary>
        /// <param name="target"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Component EnsureComponent(GameObject target, Type type)
        {
            Component comp = target.GetComponent(type);
            if (comp == null)
            {
                return target.AddComponent(type);
            }
            return comp;
        }

        /// <summary>
        /// 寻找target下指定路径上对象的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T FindComponent<T>(GameObject target, string path) where T : Component
        {
            GameObject obj = FindGameObject(target, path);
            if (obj != null)
            {
                return obj.GetComponent<T>();
            }
            return default(T);
        }

        /// <summary>
        /// 获取target下指定路径上的对象
        /// </summary>
        /// <param name="target"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static GameObject FindGameObject(GameObject target, string path)
        {
            if (target != null)
            {
                Transform t = target.transform.Find(path);
                if (t != null)
                {
                    return t.gameObject;
                }
            }

            return null;

        }

        /// <summary>
        /// 在指定root下寻找为name的GameObject，只返回第一个找到的
        /// </summary>
        /// <param name="name"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static GameObject FindGameObjbyName(string name, GameObject root)
        {
            if (root == null)
            {
                return GameObject.Find(name);
            }

            Transform[] childs = root.GetComponentsInChildren<Transform>();

            foreach (Transform trans in childs)
            {
                if (trans.gameObject.name.Equals(name))
                {
                    return trans.gameObject;
                }
            }

            return null;
        }

        /// <summary>
        /// 在指定root下寻找指定前缀的GameObject，只返回第一个找到的
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static GameObject FindFirstGameObjByPrefix(string prefix, GameObject root)
        {
            Transform[] childs;
            if (root != null)
            {
                childs = root.GetComponentsInChildren<Transform>();
            }
            else
            {
                childs = GameObject.FindObjectsOfType<Transform>();
            }

            foreach (Transform trans in childs)
            {
                if (trans.gameObject.name.Length >= prefix.Length)
                {
                    if (trans.gameObject.name.Substring(0, prefix.Length) == prefix)
                    {
                        return trans.gameObject;
                    }
                }

            }

            return null;
        }
    }
}

