using UnityEngine;
using SGF;

namespace Kurisu.Game.Entity.Factory
{
    public static class ViewFactory
    {
        public static bool EnableLog = false;
        private const string LOG_TAG = "ViewFactory";
        private static bool m_isInit = false;
        private static Transform m_viewRoot;
        private static Recycler m_recycler;

        private static DictionaryEx<EntityObject, ViewObject> m_objMap;

        public static void Init(Transform viewRoot)
        {
            if (m_isInit)
                return;

            m_isInit = true;

            m_viewRoot = viewRoot;

            m_objMap = new DictionaryEx<EntityObject, ViewObject>();
            m_recycler = new Recycler();
        }

        /// <summary>
        /// 释放工厂创建的所有对象，包括空闲对象
        /// </summary>
        public static void Release()
        {
            m_isInit = false;

            foreach (var pair in m_objMap)
            {
                pair.Value.ReleaseInFactory();
                pair.Value.Dispose();
            }

            m_objMap.Clear();
            m_recycler.Release();
            m_viewRoot = null;
        }

        public static void CreateView(string resPath, string resDefaultPath, EntityObject entity, Transform parent = null)
        {
            ViewObject viewObj = null;
            string recycleType = resPath;
            bool useRecycler = true;

            viewObj = m_recycler.Pop(recycleType) as ViewObject;
            if (viewObj == null)
            {
                useRecycler = false;
                viewObj = InstanceViewFromPrefab(recycleType, resDefaultPath);
            }

            if (viewObj == null)
                return;

            if (!viewObj.gameObject.activeSelf)
            {
                viewObj.gameObject.SetActive(true);
            }

            viewObj.transform.SetParent(parent != null ? parent : m_viewRoot, false);

            viewObj.CreateInFactory(entity, recycleType);

            if (EnableLog && Debugger.EnableLog)
            {
                Debugger.Log(LOG_TAG, "CreateView() {0}:{1} -> {2}:{3}, UseRecycler:{4}",
                    entity.GetType().Name, entity.GetHashCode(),
                    viewObj.GetRecycleType(), viewObj.GetInstanceID(),
                    useRecycler);
            }

            if (m_objMap.ContainsKey(entity))
            {
                Debugger.LogError(LOG_TAG, "CreateView() 不应该存在重复的映射！");
            }

            m_objMap[entity] = viewObj;
        }

        public static void ReleaseView(EntityObject entity)
        {
            if (entity == null)
                return;

            ViewObject viewObj;
            if ((viewObj = m_objMap[entity]) == null)
                return;

            if (EnableLog && Debugger.EnableLog)
            {
                Debugger.Log(LOG_TAG, "ReleaseView() {0}:{1} -> {2}:{3}",
                    entity.GetType().Name, entity.GetHashCode(),
                    viewObj.GetRecycleType(), viewObj.GetInstanceID());
            }

            m_objMap.Remove(entity);
            viewObj.ReleaseInFactory();
            viewObj.gameObject.SetActive(false);

            // 将对象存入到对象池中
            m_recycler.Push(viewObj);
        }

        private static ViewObject InstanceViewFromPrefab(string prefabName, string defaultPrefabName)
        {
            GameObject prefab = Resources.Load<GameObject>(prefabName);

            if (prefab == null)
            {
                prefab = Resources.Load<GameObject>(defaultPrefabName);
            }

            if (prefab == null)
            {
                return null;
            }

            GameObject go = GameObject.Instantiate(prefab);
            ViewObject instance = go.GetComponent<ViewObject>();

            if (instance == null)
            {
                Debugger.LogError(LOG_TAG, "InstanceViewFromPrefab() prefab = {0} do not find!", prefabName);
            }

            return instance;
        }
    }
}

