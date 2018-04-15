using UnityEngine;
using System.Collections;

/// <summary>
/// 定义游戏相关的常量
/// </summary>
namespace Kurisu.Game
{
    public static class GameTagDefine
    {
        /// <summary>
        /// 障碍
        /// </summary>
        public const string OBSTACLE = "Obstacle";

        /// <summary>
        /// 终点
        /// </summary>
        public const string END = "End";

        /// <summary>
        /// 玩家
        /// </summary>
        public const string PLAYER = "Player";
    }


    /// <summary>
    /// 玩家游戏状态
    /// </summary>
    public enum PlayerGameState
    {
        /// <summary>
        /// 正常状态
        /// </summary>
        Normal,

        /// <summary>
        /// 死亡状态
        /// </summary>
        Death,

        /// <summary>
        /// 到达终点了
        /// </summary>
        ArrivedAtTheEnd,
    }
}

