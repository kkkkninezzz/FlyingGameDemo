using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Kurisu.Game.Data;

using SGF.Utils;

namespace Kurisu.GameEditor.Map
{

    public class ChapterEditor : MonoBehaviour
    {
        /// <summary>
        /// 地图模式
        /// 
        /// 注：EndlessMode下不包含章节编号与小章节编号
        /// </summary>
        [Title("地图的模式")]
        [InfoBox("注：EndlessMode下不包含章节编号与小章节编号")]
        public MapMode Mode;

        /// <summary>
        /// 章节编号
        /// </summary>
        [Title("章节编号")]
        public int ChapterNo;

        /// <summary>
        /// 小章节编号，填入-1表示没有小章节
        /// </summary>
        [Title("小章节编号")]
        [InfoBox("注：填入-1表示没有小章节")]
        public int SmallChapterNo = -1;

        /// <summary>
        /// 章节名称
        /// </summary>
        [Title("章节名称")]
        public string ChapterName;
       

        /// <summary>
        /// 导出章节配置
        /// </summary>
        [Button("导出ChapterConfig")]
        public void ExportConfig()
        {
            switch(Mode)
            {
                case MapMode.ChapterMode:
                    ExportChapterModeConfig();break;
                case MapMode.EndlessMode:
                    ExportEndlessModeConfig(); break;
            }
        }

        /// <summary>
        /// 导出关卡模式的配置
        /// </summary>
        private void ExportChapterModeConfig()
        {

        }

        /// <summary>
        /// 导出无尽模式的配置
        /// </summary>
        private void ExportEndlessModeConfig()
        {
            Transform mapParts = transform.Find(ChapterEditorDef.MapParts);
            if (mapParts == null)
            {
                Debug.LogError("MapParts 结点不存在，导出失败!!!");
                return;
            }

            if (mapParts.childCount <= 0)
            {
                Debug.LogError("MapParts 结点下不存在 MapPart 结点，导出失败!!!");
                return;
            }

            List<MapPartData> mapPartDataList = new List<MapPartData>(mapParts.childCount);

            foreach (Transform mapPart in mapParts)
            {
                mapPartDataList.Add(GenerateMapPartData(mapPart));
            }

            Debug.Log(mapPartDataList.Count);
        }

        #region 构建MapPartData
        private MapPartData GenerateMapPartData(Transform mapPart)
        {
            MapPartData mapPartData = new MapPartData();

            mapPartData.startPosition = GenerateStartPosition(mapPart);
            mapPartData.endPosition = GenerateEndPosition(mapPart);
            mapPartData.basicPart = GenerateBasicPart(mapPart);
            mapPartData.randomGameObjectPool = GenerateRandomGameObjectPool(mapPart);

            return mapPartData;
        }

        private Vector3 GenerateStartPosition(Transform mapPart)
        {
            // 获取StartPosition的数据
            Transform startPosition = mapPart.Find(ChapterEditorDef.StartPosition);
            if (startPosition == null)
            {
                Debug.LogError(mapPart.name + " 下不存在 StartPosition，使用默认值!!!");
                return Vector3.zero;
            }
            else
            {
                return startPosition.position;
            }
        }

        private Vector3 GenerateEndPosition(Transform mapPart)
        {
            // 获取StartPosition的数据
            Transform endPosition = mapPart.Find(ChapterEditorDef.EndPosition);
            if (endPosition == null)
            {
                Debug.LogError(mapPart.name + " 下不存在 EndPosition，使用默认值!!!");
                return Vector3.zero;
            }
            else
            {
                return endPosition.position;
            }
        }

        private GameObjectData GenerateBasicPart(Transform mapPart)
        {
            Transform basicPart = mapPart.Find(ChapterEditorDef.BasicPart);
            if (basicPart == null)
            {
                throw new Exception(mapPart.name + " 下不存在 BasicPart，请确保 BasicPart 存在!!!");
            }

            if (basicPart.childCount <= 0)
            {
                return null;
            }

            // BasicPart下的只能有一个子对象
            return GenerateGameObjectData(basicPart.GetChild(0));
        }

        private List<List<GameObjectData>> GenerateRandomGameObjectPool(Transform mapPart)
        {
            Transform randomGameObjectPool = mapPart.Find(ChapterEditorDef.RandomGameObjectPool);
            if (randomGameObjectPool == null || randomGameObjectPool.childCount <= 0)
            {
                return new List<List<GameObjectData>>();
            }

            List<List<GameObjectData>> pool = new List<List<GameObjectData>>(randomGameObjectPool.childCount);

            foreach (Transform randomGameObjects in randomGameObjectPool)
            {
                if (randomGameObjects.childCount <= 0)
                    continue;

                List<GameObjectData> objs = new List<GameObjectData>(randomGameObjects.childCount);
                foreach (Transform trans in randomGameObjects)
                {
                    objs.Add(GenerateGameObjectData(trans));
                }

                pool.Add(objs);
            }

            return pool;
        }

        private List<RandomGameObjectData> GenerateDynamicGameObjects(Transform mapPart)
        {
            Transform dynamicGameObjects = mapPart.Find(ChapterEditorDef.DynamicGameObjects);
            if (dynamicGameObjects == null || dynamicGameObjects.childCount <= 0)
            {
                return new List<RandomGameObjectData>();
            }

            List<RandomGameObjectData> objs = new List<RandomGameObjectData>(dynamicGameObjects.childCount);

            foreach (Transform probabilityObj in dynamicGameObjects)
            {
                float probability = float.Parse(probabilityObj.name);

            }

            return objs;
        }

        private GameObjectData GenerateGameObjectData(Transform trans)
        {
            GameObjectData goData = new GameObjectData();

            SetValueToGameObjectData(trans, goData);

            return goData;
        }

        private void SetValueToGameObjectData(Transform trans, GameObjectData goData)
        {
            string prefabPath = GameObjectUtils.FindPrefabPathByGameObject(trans.gameObject);
            if (prefabPath == null)
            {
                throw new Exception(trans + " 不存在对应的 prefab!!!");
            }

            goData.Path = prefabPath;
            goData.TransformData = GenerateTransformData(trans);
        }

        private RandomGameObjectData GenerateRandomGameObjectData(Transform trans, float probability)
        {
            RandomGameObjectData randomObjData = new RandomGameObjectData();

            SetValueToGameObjectData(trans, randomObjData);
            randomObjData.Probability = probability;

            return randomObjData;
        }

        private TransformData GenerateTransformData(Transform trans)
        {
            TransformData data = new TransformData();

            data.position = trans.position;
            data.rotation = trans.rotation;
            data.scale = trans.localScale;

            return data;
        }
        #endregion

    }
}

