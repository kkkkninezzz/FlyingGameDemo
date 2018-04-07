using System;
using System.Collections.Generic;
using UnityEngine;
using SGF;

namespace Kurisu.Game.Entity.Factory
{
    /// <summary>
    /// 实体工厂，用于创建实体对象
    /// 以及后续可能对其回收和重复利用
    /// </summary>
    public static class EntityFactory
    {
        public static bool EnableLog = false;
        private static string LOG_TAG = "EntityFactory";

        private static bool m_isInit = false;
        private static Recycler m_recycler;

        /// <summary>
        /// 工厂所实例化的对象列表
        /// </summary>
        private static List<EntityObject> m_objectList;

        public static void Init()
        {
            if (m_isInit)
                return;

            m_isInit = true;
            m_objectList = new List<EntityObject>();
            m_recycler = new Recycler();
        }

        /// <summary>
        /// 释放工厂所创建的所有对象，包括空闲对象
        /// </summary>
        public static void Release()
        {
            m_isInit = false;

            for (int i = 0; i < m_objectList.Count; i++)
            {
                m_objectList[i].ReleaseInFactory();
                m_objectList[i].Dispose();
            }

            m_objectList.Clear();

            m_recycler.Release();
        }

        /// <summary>
        /// 实例化一个实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T InstanceEntity<T>() where T : EntityObject, new()
        {
            EntityObject obj = null;
            bool useRecycler = true;

            // 先从回收池中寻找
            Type type = typeof(T);
            obj = m_recycler.Pop(type.FullName) as EntityObject;
            if (obj == null)
            {
                useRecycler = false;
                obj = new T();
            }
            obj.InstanceInFactory();

            m_objectList.Add(obj);

            if (EnableLog && Debugger.EnableLog)
            {
                Debugger.Log(LOG_TAG, "InstanceEntity() {0}:{1}, UseRecycler:{2}", obj.GetType().Name, obj.GetHashCode(), useRecycler);
            }

            return obj as T;
        }

        /// <summary>
        /// 释放一个实例
        /// </summary>
        /// <param name="obj"></param>
        public static void ReleaseEntity(EntityObject obj)
        {
            if (obj == null)
                return;

            if (EnableLog && Debugger.EnableLog)
            {
                Debugger.Log(LOG_TAG, "ReleaseEntity() {0}:{1}", obj.GetType().Name, obj.GetHashCode());
            }

            obj.ReleaseInFactory();
            // 这里不立即从listObject中删除
            // 而是在下一个逻辑循环统一进行删除
            // 这样做可以提高效率
        }

        /// <summary>
        /// 清理以及被释放掉的实例，并且对其进行回收
        /// </summary>
        public static void ClearReleasedObjects()
        {
            for (int i = m_objectList.Count - 1; i >= 0; i--)
            {
                if (m_objectList[i].IsReleased)
                {
                    EntityObject obj = m_objectList[i];
                    m_objectList.RemoveAt(i);

                    // 将对象存入对象池
                    m_recycler.Push(obj);
                }
            }
        }
    }

}
