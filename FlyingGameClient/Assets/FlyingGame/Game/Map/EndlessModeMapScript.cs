using System;
using System.Collections.Generic;
using Kurisu.Game.Data;
using UnityEngine;
using Kurisu.Game.Entity.Common;
using Kurisu.Game.Entity.Factory;
using SGF;

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
        /// 在每一帧的时候会去判断是否要释放之前的地图块
        /// </summary>
        public bool ReleaseLast = false;

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
        private Vector3Data m_nextPartStart = new Vector3Data(0, 0, 0);

        /// <summary>
        /// 每一个List元素为一段地图
        /// </summary>
        private LinkedList<List<CommonEntity>> m_mapParts = new LinkedList<List<CommonEntity>>();

        public EndlessModeMapScript(EndlessModeMapData data, Transform container) : base(data, container)
        {
            
        }

        /// <summary>
        /// 进行初次加载
        /// 创建部分地图块
        /// </summary>
        public override void FirstLoad()
        {
            LoadSkybox();
            LoadMapParts();
        }

        #region 加载地图块
        private void LoadMapParts()
        {
            List<MapPartData> mapParts = m_data.mapParts;

            for (int times = 0; times < m_countOfSingleLoad; times++)
            {
                MapPartData partData = mapParts[m_mapPartIndex];
                Vector3Data startPosition = AddVector3Data(m_nextPartStart, partData.startPosition);

                m_mapParts.AddLast(LoadMapPart(partData, startPosition));

                // 更新下一块地图块的开始位置
                m_nextPartStart = AddVector3Data(m_nextPartStart, partData.endPosition);

                m_mapPartIndex++;
                if (m_mapPartIndex == mapParts.Count)
                {
                    m_mapPartIndex = 0;
                }

            }
        }
       
        #endregion


        public override void EnterFrame(int frameIndex)
        {
            if (LoadNext)
            {
                LoadMapParts();
                LoadNext = false;
            }

            if (ReleaseLast)
            {
                ReleaseLastMapPart();
                ReleaseLast = false;
            }
        }

        private void ReleaseLastMapPart()
        {
            if (m_mapParts.Count <= 0)
            {
                return;
            }

            // 释放当前最早生成的地图段
            List<CommonEntity> lastMapPart = m_mapParts.First.Value;
            m_mapParts.RemoveFirst();
            
            foreach (CommonEntity entity in lastMapPart)
            {
                EntityFactory.ReleaseEntity(entity);
            }
        }

    }
}
