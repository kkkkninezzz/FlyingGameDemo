using Kurisu.Game.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kurisu.Game.Map
{
    public class GameMap
    {
        private MapScript m_script;

        private GameObject m_view;

        /// <summary>
        /// 通过MapData加载地图
        /// </summary>
        /// <param name="data"></param>
        public void Load(MapData data)
        {

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
    }
}

