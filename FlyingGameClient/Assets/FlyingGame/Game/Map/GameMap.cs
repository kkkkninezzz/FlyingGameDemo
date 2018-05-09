using Kurisu.Game.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
                    // TODO 实现正常模式的
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
            m_script.FirstLoad();
        }
      
        /// <summary>
        /// 卸载地图
        /// </summary>
        public void Unload()
        {
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

        public GameObject View
        {
            get
            {
                return m_view;
            }
        }

        public List<TransformData> BirthPoints
        {
            get
            {   
                return m_data.birthPoints;
            }
        }
    }
}

