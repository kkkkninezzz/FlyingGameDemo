using UnityEngine;
using System.Collections;

namespace Kurisu.Game.Data
{
    /// <summary>
    /// 游戏的输入
    /// </summary>
    public enum GameVkey
    {
        #region 玩家的输入
        /// <summary>
        /// 在垂直方向移动
        /// </summary>
        MoveVertical,

        /// <summary>
        /// 在水平线方向移动
        /// </summary>
        MoveHorizontal,

        /// <summary>
        /// 加速
        /// </summary>
        SpeedUp,

        /// <summary>
        /// 减速
        /// </summary>
        SpeedDown,
        #endregion

        #region 全局输入
        /// <summary>
        /// 创建玩家
        /// </summary>
        CreatePlayer,

        /// <summary>
        /// 销毁玩家
        /// </summary>
        ReleasePlayer,
        #endregion
    }
}

