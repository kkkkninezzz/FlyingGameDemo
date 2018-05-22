using Kurisu.Game.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Kurisu.Service.Audio;

namespace Kurisu.Game.Map
{
    public class GameMap
    {

        private MapScript m_script;

        private GameObject m_view;

        private MapData m_data;

        /// <summary>
        /// 初始化地图
        /// </summary>
        /// <param name="data"></param>
        public void Init(MapData data)
        {
            m_data = data;
            m_view = new GameObject("map");

            switch (data.mapMode)
            {
                case MapMode.EndlessMode:
                    m_script = new EndlessModeMapScript((EndlessModeMapData)data, m_view.transform);
                    break;
                case MapMode.NormalMode:
                    m_script = new NormalModeMapScript((NormalModeMapData)data, m_view.transform);
                    break;
                default:
                    throw new Exception(string.Format("未知的地图模式 {0}", data.mapMode));
            }
        }

        /// <summary>
        /// 加载地图
        /// </summary>
        public void Load()
        {
            LoadSkybox();
            LoadBgms();
            m_script.FirstLoad();
        }
      
        /// <summary>
        /// 卸载地图
        /// </summary>
        public void Unload()
        {
            UnloadBgms();
            m_script = null;
            if (m_view != null)
            {
                GameObject.Destroy(m_view);
                m_view = null;
            }
        }

        public void EnterFrame(int frameIndex)
        {
            if (m_script != null)
            {
                m_script.EnterFrame(frameIndex);
            }
        }

        #region 属性
        public GameObject View
        {
            get
            {
                return m_view;
            }
        }

        public MapScript MapScript
        {
            get
            {
                return m_script;
            }
        }

        public List<TransformData> BirthPoints
        {
            get
            {
                return m_data.birthPoints;
            }
        }
        #endregion

        /// <summary>
        /// 加载天空盒
        /// </summary>
        private void LoadSkybox()
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

        /// <summary>
        /// 加载背景音乐
        /// </summary>
        private void LoadBgms()
        {
            List<string> bgmPaths = m_data.bgmPaths;
            if (bgmPaths == null || bgmPaths.Count <= 0)
            {
                return;
            }

            List<AudioClip> bgms = new List<AudioClip>(m_data.bgmPaths.Count);
            foreach (string bgmPath in bgmPaths)
            {
                AudioClip bgm = Resources.Load<AudioClip>(bgmPath);
                if (bgm != null)
                {
                    bgms.Add(bgm);
                }
            }

            AudioManager.Instance.AutoPlayBgms(bgms);
        }

        /// <summary>
        /// 卸载背景音乐
        /// </summary>
        private void UnloadBgms()
        {
            AudioManager.Instance.ReleaseBgms();
        }
    }
}

