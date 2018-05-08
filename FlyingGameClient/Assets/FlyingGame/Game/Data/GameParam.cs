using System.Collections;
using System;

namespace Kurisu.Game.Data
{
    [Serializable]
    public class GameParam
    {
        /// <summary>
        /// GameId，服务器会允许同时开始多场游戏
        /// 每一局游戏都有一个编号，在PVP时会用到
        /// </summary>
        public int id = 0;

        /// <summary>
        /// 地图数据，决定这场游戏用哪个地图
        /// </summary>
        public MapData mapData;

        /// <summary>
        /// 随机数种子，用于不同的客户端 产生相同的随机数
        /// </summary>
        public int randSeed = 0;

        /// <summary>
        /// 游戏模式
        /// </summary>
        public GameMode mode = GameMode.NormalPVE;

        /// <summary>
        /// 限时模式的最大时间
        /// </summary>
        public int limitedTime = 180; // Second
    }
}

