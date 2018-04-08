
namespace Kurisu.Game
{   /// <summary>
    /// 定义游戏单局中会向外部模块抛出的事件
    /// </summary>

    /// <summary>
    /// 玩家死亡事件
    /// </summary>
    /// <param name="playerId"></param>
    public delegate void PlayerDieEvent(uint playerId);

    /// <summary>
    /// 玩家胜利事件
    /// </summary>
    /// <param name="playerId"></param>
    public delegate void PlayerWinEvent(uint playerId);

    /// <summary>
    /// 玩家失败事件
    /// </summary>
    /// <param name="palyerId"></param>
    public delegate void PlayerFailedEvent(uint palyerId);
}

