using System;
using System.Collections.Generic;
using Kurisu.Game.Data;
using UnityEngine;

namespace Kurisu.Game.Map
{
    /// <summary>
    /// 无尽模式的地图脚本
    /// </summary>
    public class EndlessModeMapScript : AbstractMapScript<EndlessModeMapData>
    {
        /// <summary>
        /// 在每一帧的时候会去判断是否需要加载下一段地图
        /// </summary>
        public bool LoadNext = false;

        /// <summary>
        /// 每一次加载地图的块数
        /// </summary>
        private int m_countOfSingleLoad = 2;

        /// <summary>
        /// 下一块地图块的下标
        /// </summary>
        private int m_mapPartIndex = 0;

        /// <summary>
        /// 下一块地图块的开始位置
        /// </summary>
        private Vector3 m_nextPartStart = Vector3.zero;

        public EndlessModeMapScript(EndlessModeMapData data, Transform container) : base(data, container)
        {
            
        }

        /// <summary>
        /// 进行初次加载
        /// 创建部分地图块
        /// </summary>
        public override void FirstLoad()
        {
            LoadMapParts();
        }

        private void LoadMapParts()
        {
            List<MapPartData> mapParts = m_data.mapParts;

            for (int times = 0; times < m_countOfSingleLoad; times++)
            {
                MapPartData partData = mapParts[m_mapPartIndex];

                // 实例化MapPart

                m_mapPartIndex++;
                if (m_mapPartIndex == mapParts.Count)
                {
                    m_mapPartIndex = 0;
                }
            }
        }

        public override void EnterFrame(int frameIndex)
        {
            if (LoadNext)
            {
                LoadMapParts();
                LoadNext = false;
            }
        }

    }
}
