using UnityEngine;
using System.Collections;
using Kurisu.Game.Entity.Common;
using Kurisu.Module.Pve;
using Kurisu.Module;
using Kurisu.Game.Data;

namespace Kurisu.Game.Entity.Map
{
    /// <summary>
    /// 拼图的视图
    /// </summary>
    public class PuzzleView : CommonView
    {
        /// <summary>
        /// 该拼图的分数
        /// </summary>
        public uint Score = 100;

        /// <summary>
        /// 值多少个拼图
        /// </summary>
        public uint PuzzleCount = 1;

        public void OnTriggerEnter(Collider other)
        {
            if (!GameTagDefine.PLAYER.Equals(other.tag))
            {
                return;
            }
            GameLogicManager gameManager = GameLogicManager.Instance;
            GameMode gameMode = gameManager.GameMode;

            if (gameMode == GameMode.EndlessPVE || gameMode == GameMode.NormalPVE || gameMode == GameMode.TimelimitPVE)
            {
                PveModule pveModule = ModuleAPI.PveModule;
                pveModule.IncreaseScore(Score);
                pveModule.IncreasePuzzle(PuzzleCount);
            }

            // 设置为隐藏
            this.gameObject.SetActive(false);
        }
    }
}

