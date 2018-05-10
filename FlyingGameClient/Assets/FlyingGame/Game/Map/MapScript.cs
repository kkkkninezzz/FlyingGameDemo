using UnityEngine;
using System.Collections.Generic;
using Kurisu.Game.Data;
using Kurisu.Game.Entity.Common;
using Kurisu.Game.Entity.Factory;
using SGF;

namespace Kurisu.Game.Map
{
    public interface MapScript
    {
        /// <summary>
        /// 第一次加载
        /// </summary>
        void FirstLoad();

        void EnterFrame(int frameIndex);
    }

    public abstract class AbstractMapScript<T> : MapScript where T : MapData
    {
        protected T m_data;
        protected Transform m_container;

        public AbstractMapScript(T data, Transform container)
        {
            m_data = data;
            m_container = container;
        }

        #region 加载地图块
        protected List<CommonEntity> LoadMapPart(MapPartData partData, Vector3Data startPosition)
        {
            List<CommonEntity> mapPart = new List<CommonEntity>();
            startPosition = AddVector3Data(startPosition, partData.startPosition);

            // 加载BasicPart
            LoadGameObject(mapPart, startPosition, partData.basicPart);
            // 从randomGameObjectPool加载数据
            LoadFromRandomGameObjectPool(mapPart, startPosition, partData.randomGameObjectPool);
            // 从dynamicGameObjects加载数据
            LoadFormDynamicGameObjects(mapPart, startPosition, partData.dynamicGameObjects);

            return mapPart;
        }

        private void LoadGameObject(List<CommonEntity> mapPart, Vector3Data startPositon, GameObjectData data)
        {
            CommonEntity entity = EntityFactory.InstanceEntity<CommonEntity>();
            entity.Create(m_container, data.path);

            data.transformData.position = AddVector3Data(startPositon, data.transformData.position);
            entity.InitTransform(data.transformData);

            mapPart.Add(entity);
        }

        protected Vector3Data AddVector3Data(Vector3Data v1, Vector3Data v2)
        {
            return new Vector3Data(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        private void LoadFromRandomGameObjectPool(List<CommonEntity> mapPart, Vector3Data startPositon, List<List<GameObjectData>> randomGameObjectPool)
        {
            if (randomGameObjectPool == null || randomGameObjectPool.Count <= 0)
            {
                return;
            }

            List<GameObjectData> objs = randomGameObjectPool[SGFRandom.Default.Range(0, randomGameObjectPool.Count)];

            if (objs == null || objs.Count <= 0)
            {
                return;
            }

            foreach (GameObjectData data in objs)
            {
                LoadGameObject(mapPart, startPositon, data);
            }
        }

        private void LoadFormDynamicGameObjects(List<CommonEntity> mapPart, Vector3Data startPositon, List<RandomGameObjectData> dynamicGameObjects)
        {
            if (dynamicGameObjects == null || dynamicGameObjects.Count <= 0)
            {
                return;
            }

            foreach (RandomGameObjectData data in dynamicGameObjects)
            {
                float probability = data.probability;
                // 如果概率大于等于1或者随机出来的概率大于目标概率，则进行创建;
                if (probability >= 1 || SGFRandom.Default.Range(0f, 1f) >= probability)
                {
                    LoadGameObject(mapPart, startPositon, data);
                }
            }
        }
        #endregion

        /// <summary>
        /// 加载天空盒
        /// </summary>
        protected void LoadSkybox()
        {
            string skyboxPath = m_data.skyboxPath;
            if (string.IsNullOrEmpty(skyboxPath))
            {
                return;
            }

            Material skybox = Resources.Load<Material>(skyboxPath);
            if (skybox != null)
            {
                RenderSettings.skybox = skybox;
            }
        }

        public abstract void FirstLoad();
        public abstract void EnterFrame(int frameIndex);
    }
}

