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
        /// 背景音乐列表
        /// </summary>
        [Title("背景音乐列表")]
        [InfoBox("注：如果有多首背景音乐，则在一首播放完毕后会自动播放下一首")]
        [AssetList(Path = "/Resources/audio/Bgm/")]
        public List<AudioClip> BgmList;

        #region 天空盒
        /// <summary>
        /// 天空盒材质
        /// </summary>
        [Title("天空盒材质")]
        [CustomContextMenu("应用天空盒", "ApplySkybox")]
        [CustomContextMenu("重置天空盒", "ResetSkybox")]
        [AssetList(Path = "/Resources/material/Skybox/")]
        public Material SkyBox;

        /// <summary>
        /// 应用天空盒
        /// </summary>
        public void ApplySkybox()
        {
            if (SkyBox == null)
            {
                return;
            }
            
            RenderSettings.skybox = SkyBox;
        }

        /// <summary>
        /// RemoveSkyBox
        /// </summary>
        public void ResetSkybox()
        {
            Material DefaultSkyBox = Resources.Load<Material>("material/Skybox/Sunny_01_A");
            RenderSettings.skybox = DefaultSkyBox;
            SkyBox = DefaultSkyBox;
        }
        #endregion




        /// <summary>
        /// 导出章节配置
        /// </summary>
        [Button("导出ChapterConfig")]
        public void ExportConfig()
        {
            ModeMapData mapData = null;

            Transform mapParts = transform.Find(ChapterEditorDef.MapParts);
            if (mapParts == null)
            {
                throw new Exception("MapParts 结点不存在，导出失败!!!");
            }

            if (mapParts.childCount <= 0)
            {
                throw new Exception("MapParts 结点下不存在 MapPart 结点，导出失败!!!");

            }

            switch(Mode)
            {
                case MapMode.ChapterMode:
                    ChapterModeMapData chapterModeMapData = new ChapterModeMapData();
                    chapterModeMapData.mapPart = GenerateMapPartDataList(mapParts, true)[0];
                    mapData = chapterModeMapData;
                    break;

                case MapMode.EndlessMode:
                    EndlessModeMapData endlessModeMapData = new EndlessModeMapData();
                    endlessModeMapData.mapParts = GenerateMapPartDataList(mapParts, false);
                    mapData = endlessModeMapData;
                    break;
            }

            if (mapData == null)
            {
                throw new Exception("出现了未知的错误，构建数据失败!!!");
            }

            // 背景音乐路径
            mapData.bgmPaths = GenerateBgmPaths();
            // 天空盒路径
            mapData.skyboxPath = GenerateSkyboxPath();

            // 出生点信息
        }

        /// <summary>
        /// 构建MapPartDataList
        /// </summary>
        /// <param name="mapParts"></param>
        /// <param name="isChapterMode"></param>
        /// <returns></returns>
        private List<MapPartData> GenerateMapPartDataList(Transform mapParts, bool isChapterMode)
        {
            List<MapPartData> mapPartDataList;
            if (isChapterMode)
            {
                mapPartDataList = new List<MapPartData>(1);
                mapPartDataList.Add(GenerateMapPartData(mapParts.GetChild(0)));
            } 
            else
            {
                mapPartDataList = new List<MapPartData>(mapParts.childCount);

                foreach (Transform mapPart in mapParts)
                {
                    mapPartDataList.Add(GenerateMapPartData(mapPart));
                }
            }

            return mapPartDataList;
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


        private List<string> GenerateBgmPaths()
        {
            if (BgmList == null || BgmList.Count == 0)
                return new List<string>(0);

            List<string> bgmPaths = new List<string>(BgmList.Count);

            foreach (AudioClip bgm in BgmList)
            {
                bgmPaths.Add(GameObjectUtils.FindAssetPath(bgm));
            }

            return bgmPaths;
        }

        private string GenerateSkyboxPath()
        {
            if (SkyBox == null)
            {
                return null;
            }

            return GameObjectUtils.FindAssetPath(SkyBox);
        }

        [Button("ShowBgmPaths")]
        public void ShowBgmPaths()
        {
            if (BgmList == null || BgmList.Count == 0)
            {
                return;
            }

            foreach (AudioClip bgm in BgmList)
            {
                Debug.Log(GameObjectUtils.FindAssetPath(bgm));
            }
        }
        [Button("LoadBgm")]
        public void LoadBgm()
        {
            AudioClip bgm = Resources.Load<AudioClip>("audio/Bgm/RADWIMPS - 前前前世 (movie ver.)");
            Debug.Log(bgm.name);
        }
    }
}

