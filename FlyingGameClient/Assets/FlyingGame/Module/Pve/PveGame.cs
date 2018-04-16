using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using SGF;
using Kurisu.Game;
using Kurisu.Game.Data;

namespace Kurisu.Module.Pve
{
    public class PveGame
    {

        private uint m_mainPlayerId = 1;
        private int m_frameIndex = 0;
        private bool m_pause = false;

        public event Action onMainPlayerDie;
        public event Action onGameEnd;

        private GameContext m_context;

        public void Start(GameParam param)
        {
            GameLogicManager gameManaager = GameLogicManager.Instance;

            gameManaager.CreateGame(param);
            gameManaager.onPlayerDie += OnPlayerDie;

            // TODO 初始玩家数据

        }

        //--------------------------------------------------
        /// <summary>
        /// 当玩家死亡时，进行处理
        /// </summary>
        /// <param name="playerId"></param>
		private void OnPlayerDie(uint playerId)
        {
            if (m_mainPlayerId == playerId)
            {
                // Pause();

                if (onMainPlayerDie != null)
                {
                    onMainPlayerDie();
                }
                else
                {
                    this.LogError("OnPlayerDie() onMainPlayerDie == null!");
                }
            }
        }
    }
}

