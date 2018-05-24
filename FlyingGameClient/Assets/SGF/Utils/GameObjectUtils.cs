using UnityEngine;
using System;
using Kurisu.Game.Data;

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

        

        /// <summary>
        /// 转换为Vector3
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(Vector3Data data)
        {
            return new Vector3(data.x, data.y, data.z);
        }

        /// <summary>
        /// 转换为Vector3Data
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3Data ToVector3Data(Vector3 v)
        {
            return new Vector3Data(v.x, v.y, v.z);
        }

        /// <summary>
        /// 转换为Quaternion
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Quaternion ToQuaternion(QuaternionData data)
        {
            return new Quaternion(data.x, data.y, data.z, data.z);
        }

        /// <summary>
        /// 转换为QuaternionData
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static QuaternionData ToQuaternionData(Quaternion q)
        {
            return new QuaternionData(q.x, q.y, q.z, q.w);
        }


        /// <summary>
        /// 转换为TransformData
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static TransformData ToTransformData(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            return new TransformData(ToVector3Data(Vector3.zero),
                                     ToQuaternionData(Quaternion.Euler(Vector3.zero)),
                                     ToVector3Data(Vector3.one));
        }

        /// <summary>
        /// 为已有的Transform设置数据
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="data"></param>
        public static void SetTransformDataForObj(Transform trans, TransformData data)
        {
            trans.position = ToVector3(data.position);
            trans.rotation = ToQuaternion(data.rotation);
            trans.localScale = ToVector3(data.scale);
        }

        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="child"></param>
        /// <param name="parent"></param>
        public static void SetParent(GameObject child, GameObject parent, bool worldPositionStays = false)
        {
            child.transform.SetParent(parent.transform, worldPositionStays);
        }
    }
}

