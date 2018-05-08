using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Kurisu.Game.Data;

using SGF.Utils;
using SGF;
using Newtonsoft.Json;

namespace Kurisu.GameEditor.Map
{

    public class ChapterEditor : MonoBehaviour
    {
        [Title("章节配置文件")]
        [InfoBox("注1: 将导出好的章节配置读取到编辑器中，请确保配置文件的合法性!!!\n" +
            "注2: 操作方式，右键 -> 读取配置")]
        [CustomContextMenu("读取配置", "LoadConfig")]
        public TextAsset ChapterConfig;

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
        /// ResetSkybox
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
            Debugger.EnableLog = true;

            this.Log("当前模式: {0}，开始构建数据...", Mode);
            MapData mapData = GenerateMapData();
            if (mapData == null)
            {
                throw new Exception("构建数据数据失败，因此拒绝导出!!!");
            }
            this.Log("构建数据结束");

            this.Log("开始导出配置");

            string path = "";
            switch (Mode)
            {
                case MapMode.NormalMode:
                    path = string.Format(ChapterEditorDef.ChapterConfigRootPath + "/{0}/{1}.json", ChapterNo, mapData.no);
                    JsonUtils.WriteDataToJsonFile(path, mapData);
                    break;

                case MapMode.EndlessMode:
                    path = string.Format(ChapterEditorDef.EndlessConfigRootPath + "{0}.json", mapData.no);
                    JsonUtils.WriteDataToJsonFile(path, mapData);
                    break;
            }

            this.Log("配置导出成功，Path = {0}", path);

            Debugger.EnableLog = false;
        }

        private MapData GenerateMapData()
        {
            MapData mapData = null;

            Transform mapParts = transform.Find(ChapterEditorDef.MapParts);
            if (mapParts == null)
            {
                throw new Exception("MapParts 结点不存在，导出失败!!!");
            }

            if (mapParts.childCount <= 0)
            {
                throw new Exception("MapParts 结点下不存在 MapPart 结点，导出失败!!!");

            }

            switch (Mode)
            {
                case MapMode.NormalMode:
                    NormalModeMapData normalModeMapData = new NormalModeMapData();
                    normalModeMapData.mapPart = GenerateMapPartDataList(mapParts, true)[0];
                    mapData = normalModeMapData;
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

            // 章节编号
            mapData.no = GenerateChapterNo();
            // 章节名称
            mapData.name = ChapterName;
            // 背景音乐路径
            mapData.bgmPaths = GenerateBgmPaths();
            // 天空盒路径
            mapData.skyboxPath = GenerateSkyboxPath();
            // 出生点信息
            mapData.birthPoints = GenerateBirthPoints();

            return mapData;
        }

        /// <summary>
        /// 构建MapPartDataList
        /// </summary>
        /// <param name="mapParts"></param>
        /// <param name="isNormalMode"></param>
        /// <returns></returns>
        private List<MapPartData> GenerateMapPartDataList(Transform mapParts, bool isNormalMode)
        {
            List<MapPartData> mapPartDataList;
            if (isNormalMode)
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

        private Vector3Data GenerateStartPosition(Transform mapPart)
        {
            // 获取StartPosition的数据
            Transform startPosition = mapPart.Find(ChapterEditorDef.StartPosition);
            if (startPosition == null)
            {
                Debug.LogError(mapPart.name + " 下不存在 StartPosition，使用默认值!!!");
                return GameObjectUtils.ToVector3Data(Vector3.zero);
            }
            else
            {
                return GameObjectUtils.ToVector3Data(startPosition.position);
            }
        }

        private Vector3Data GenerateEndPosition(Transform mapPart)
        {
            // 获取StartPosition的数据
            Transform endPosition = mapPart.Find(ChapterEditorDef.EndPosition);
            if (endPosition == null)
            {
                Debug.LogError(mapPart.name + " 下不存在 EndPosition，使用默认值!!!");
                return GameObjectUtils.ToVector3Data(Vector3.zero);
            }
            else
            {
                return GameObjectUtils.ToVector3Data(endPosition.position);
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

            goData.name = trans.name;
            goData.path = prefabPath;
            goData.transformData = GenerateTransformData(trans);
        }

        private RandomGameObjectData GenerateRandomGameObjectData(Transform trans, float probability)
        {
            RandomGameObjectData randomObjData = new RandomGameObjectData();

            SetValueToGameObjectData(trans, randomObjData);
            randomObjData.probability = probability;

            return randomObjData;
        }

        private TransformData GenerateTransformData(Transform trans)
        {
            TransformData data = new TransformData();

            data.position = GameObjectUtils.ToVector3Data(trans.position);
            data.rotation = GameObjectUtils.ToQuaternionData(trans.rotation);
            data.scale = GameObjectUtils.ToVector3Data(trans.localScale);

            return data;
        }
        #endregion


        private string GenerateChapterNo()
        {
            return "" + ChapterNo + (SmallChapterNo < 0 ? "" : "_" + SmallChapterNo);
        }

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

        private List<TransformData> GenerateBirthPoints()
        {
            Transform birthPoints = transform.Find(ChapterEditorDef.BirthPoints);

            List<TransformData> points;
            if (birthPoints == null || birthPoints.childCount <= 0)
            {
                // throw new Exception("BirthPoints 结点不存在，导出失败!!!");
                points = new List<TransformData>(1);
                points.Add(GameObjectUtils.ToTransformData(Vector3.zero, Quaternion.Euler(Vector3.zero), Vector3.one));
            } 
            else
            {
                points = new List<TransformData>(birthPoints.childCount);

                foreach (Transform point in birthPoints)
                {
                    points.Add(GenerateTransformData(point));
                }
            }

            return points;
        }

        /*
        [Button("Test")]
        public void Test()
        {
            AudioClip ac1 = Resources.Load<AudioClip>("audio/Bgm/RADWIMPS - 前前前世 (movie ver.)");

            BgmList.Add(ac1);
        }
        */

        [ContextMenu("重置所有数据")]
        public void ResetAllData()
        {
            ChapterName = "";
            if (BgmList != null)
                BgmList.Clear();
            ResetSkybox();

            Transform mapParts = this.transform.Find(ChapterEditorDef.MapParts);
            RemoveAllChildren(mapParts);

            Transform birthPoints = this.transform.Find(ChapterEditorDef.BirthPoints);
            RemoveAllChildren(birthPoints);
        }

        private void RemoveAllChildren(Transform parent)
        {
            if (parent == null)
                return;

            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(parent.GetChild(i).gameObject);
            }
        }

        #region 加载配置
        /// <summary>
        /// 加载配置
        /// </summary>
        public void LoadConfig()
        {
            if (ChapterConfig == null)
            {
                Debug.LogError("未选中章节配置文件，请先选中章节配置文件!!!");
                return;
            }
            Debugger.EnableLog = true;

            this.Log("重置所有数据中...");
            // 重置掉所有数据
            ResetAllData();
            this.Log("数据重置成功");

            this.Log("开始加载章节配置文件: {0}", ChapterConfig.name);
            
            Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ChapterConfig.text);

            MapMode mapMode = (MapMode)((long)dic["mapMode"]);
            switch (mapMode)
            {
                case MapMode.NormalMode:
                    LoadNormalModeMapData(ChapterConfig.text);
                    break;
                case MapMode.EndlessMode:
                    LoadEndlessModeMapData(ChapterConfig.text);
                    break;
            }


            this.Log("加载章节配置文件成功");
            
            Debugger.EnableLog = false;
        }

        private void LoadModeMapData(MapData mapData)
        {
            this.Mode = mapData.mapMode;

            LoadChapterNo(mapData.no);

            this.ChapterName = mapData.name;

            LoadBgms(mapData.bgmPaths);

            LoadSkybox(mapData.skyboxPath);

            LoadBirthPoints(mapData.birthPoints);
        }

        private void LoadChapterNo(string no)
        {
            if (string.IsNullOrEmpty(no))
            {
                return;
            }

            string[] chapterNo = no.Split('_');
            if (chapterNo != null && chapterNo.Length > 0 && !string.IsNullOrEmpty(chapterNo[0]))
            {
                this.ChapterNo = int.Parse(chapterNo[0]);

                if (chapterNo.Length == 2 && !string.IsNullOrEmpty(chapterNo[1]))
                {
                    this.SmallChapterNo = int.Parse(chapterNo[1]);
                }
                else
                {
                    this.SmallChapterNo = -1;
                }
            }
        }

        private void LoadBgms(List<string> bgmPaths)
        {
            if (bgmPaths == null || bgmPaths.Count <= 0)
            {
                return;
            }
            if (BgmList == null)
            {
                BgmList = new List<AudioClip>(bgmPaths.Count);
            }

            foreach (string path in bgmPaths)
            {
                
                AudioClip ac = Resources.Load<AudioClip>(path);
                if (ac != null)
                {
                    BgmList.Add(ac);
                    
                }
                
            }
        }

        private void LoadSkybox(string skyboxPath)
        {
            if (string.IsNullOrEmpty(skyboxPath))
            {
                return;
            }

            Material skybox = Resources.Load<Material>(skyboxPath);
            if (skybox != null)
            {
                this.SkyBox = skybox;
                ApplySkybox();
            }
        }

        private void LoadBirthPoints(List<TransformData> birthPoints)
        {
            if (birthPoints == null || birthPoints.Count <= 0)
            {
                return;
            }

            Transform birthPointsTrans = this.transform.Find(ChapterEditorDef.BirthPoints);
            if (birthPointsTrans == null)
            {
                GameObject bpGameObj = new GameObject(ChapterEditorDef.BirthPoints);
                bpGameObj.transform.parent = this.transform;
                birthPointsTrans = bpGameObj.transform;
            }

            GameObject birthPointPrefab = Resources.Load<GameObject>(ChapterEditorDef.BirthPointPrefabPath);
            if (birthPointPrefab == null)
            {
                throw new Exception(ChapterEditorDef.BirthPointPrefabPath + " 下没有找到相应的出生点预制体!!!");
            }

            foreach (TransformData birthPoint in birthPoints)
            {
                GameObject go = GameObject.Instantiate<GameObject>(birthPointPrefab);
                go.transform.parent = birthPointsTrans;
                GameObjectUtils.SetTransformDataForObj(go.transform, birthPoint);
            }
        }

        private void LoadMapParts(List<MapPartData> mapPartDataList)
        {
            if (mapPartDataList == null || mapPartDataList.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < mapPartDataList.Count; i++)
            {
                LoadMapPart(i, mapPartDataList[i]);
            }
        }

        #region 加载MapPart
        private void LoadMapPart(int index, MapPartData mapPartData)
        {
            GameObject mapPart = GenerateMapPart(index);
            
            // 调整StartPosition和EndPosition的位置
            mapPart.transform.Find(ChapterEditorDef.StartPosition).position = GameObjectUtils.ToVector3(mapPartData.startPosition);
            mapPart.transform.Find(ChapterEditorDef.EndPosition).position = GameObjectUtils.ToVector3(mapPartData.endPosition);

            // 加载BasicPart
            LoadBasicPart(mapPart, mapPartData.basicPart);

            // 加载RandomGameObjectPool
            LoadRandomGameObjectPool(mapPart, mapPartData.randomGameObjectPool);

            // 加载LoadDynamicGameObjects
            LoadDynamicGameObjects(mapPart, mapPartData.dynamicGameObjects);
        }

        private void LoadBasicPart(GameObject mapPart, GameObjectData basicPartData)
        {
            if (basicPartData == null)
            {
                return;
            }

            Transform basicPart = mapPart.transform.Find(ChapterEditorDef.BasicPart);

            LoadGameObjectData(basicPart, basicPartData);
        }

        private void LoadRandomGameObjectPool(GameObject mapPart, List<List<GameObjectData>> dataList)
        {
            if (dataList == null || dataList.Count <= 0)
            {
                return;
            }

            Transform randomGameObjectPool = mapPart.transform.Find(ChapterEditorDef.RandomGameObjectPool);
            for (int i =  0; i < dataList.Count; i++)
            {
                GameObject randomGo = new GameObject(ChapterEditorDef.RandomGameObjectPool + "_" + i.ToString().PadLeft(3, '0'));
                randomGo.transform.parent = randomGameObjectPool;

                for (int j = 0; j < dataList[i].Count; j++)
                {
                    LoadGameObjectData(randomGo.transform, dataList[i][j]);
                }
            }
        }

        private void LoadDynamicGameObjects(GameObject mapPart, List<RandomGameObjectData> dataList)
        {
            if (dataList == null || dataList.Count <= 0)
            {
                return;
            }

            Transform dynamicGameObjects = mapPart.transform.Find(ChapterEditorDef.DynamicGameObjects);
            foreach (RandomGameObjectData data in dataList)
            {
                float probability = data.probability;
                Transform probabilityGo = dynamicGameObjects.Find(probability.ToString());
                if (probabilityGo == null)
                {
                    throw new Exception(string.Format("当前概率 {0} 不支持!!! 请检查配置文件", probability));
                }

                LoadGameObjectData(probabilityGo, data);
            }
        }

        private void LoadGameObjectData(Transform parent, GameObjectData data)
        {
            GameObject prefab = Resources.Load<GameObject>(data.path);
            if (prefab == null)
            {
                throw new Exception("Don't have prefab in " + data.path);
            }
            
            GameObject gameObj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

            gameObj.name = data.name;
            gameObj.transform.parent = parent;
            
        }

        #endregion


        private GameObject GenerateMapPart(int index)
        {
            Transform mapParts = this.transform.Find(ChapterEditorDef.MapParts);
            if (mapParts == null)
            {
                GameObject mapPartsObj = new GameObject(ChapterEditorDef.MapParts);
                mapPartsObj.transform.parent = this.transform;
                mapParts = mapPartsObj.transform;
            }

            GameObject mapPartPrefab = Resources.Load<GameObject>(ChapterEditorDef.MapPartPrefabPath);
            if (mapPartPrefab == null)
            {
                throw new Exception("Don't have MapPartPrefab in " + ChapterEditorDef.MapPartPrefabPath);
            }

            GameObject mapPart = GameObject.Instantiate(mapPartPrefab);
            mapPart.name = ChapterEditorDef.MapPart + "_" + index.ToString().PadLeft(3, '0');
            mapPart.transform.parent = mapParts;

            return mapPart;
        }
        
        /// <summary>
        /// 加载关卡模式的地图
        /// </summary>
        /// <param name="configStr"></param>
        private void LoadNormalModeMapData(string configStr)
        {
            NormalModeMapData normalModeMapData = JsonConvert.DeserializeObject<NormalModeMapData>(configStr);
            if (normalModeMapData == null)
            {
                throw new Exception("读取配置失败!!! 请检查配置文件是否正确");
            }

            LoadModeMapData(normalModeMapData);

            LoadMapPart(0, normalModeMapData.mapPart);
        }

        /// <summary>
        /// 加载无尽模式的地图
        /// </summary>
        /// <param name="configStr"></param>
        private void LoadEndlessModeMapData(string configStr)
        {
            EndlessModeMapData endlessModeMapData = JsonConvert.DeserializeObject<EndlessModeMapData>(configStr);
            if (endlessModeMapData == null)
            {
                throw new Exception("读取配置失败!!! 请检查配置文件是否正确");
            }

            LoadModeMapData(endlessModeMapData);

            LoadMapParts(endlessModeMapData.mapParts);
        }
        #endregion

    }
}

