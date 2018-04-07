using System.Collections;
using System;

namespace Kurisu.Game.Data
{
    [Serializable]
    public class PlayerData
    {
        /// <summary>
        /// 在单局中分配的id，只在单局有效
        /// </summary>
        public uint id;

        /// <summary>
        /// 这个玩家对应的用户id
        /// </summary>
        public uint userId;

        /// <summary>
        /// 玩家的昵称
        /// </summary>
        public string name;

        /// <summary>
        /// 玩家使用的飞行载具的数据
        /// </summary>
        public FlyingVehicleData vehicleData = new FlyingVehicleData();

        /// <summary>
        /// 玩家的队伍id，如果是单人游戏，那么就等于id
        /// </summary>
        public uint teamId;

        /// <summary>
        /// 玩家本局获得分数
        /// </summary>
        public int score;

        /// <summary>
        /// 如果这个玩家挂机，或者是AI玩家的话，其AI的ID，如果0则是不使用AI
        /// </summary>
        public int ai;
    }
}

