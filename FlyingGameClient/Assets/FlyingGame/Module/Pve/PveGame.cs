using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using SGF;
using Kurisu.Game;
using Kurisu.Game.Data;
using Kurisu.Game.Map;

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
            GameLogicManager gameManager = GameLogicManager.Instance;

            gameManager.CreateGame(param);
            gameManager.onPlayerDie += OnPlayerDie;
            m_context = gameManager.Context;

            // 初始玩家数据
            InitPalyerData();

            InitGameInput();
        }

        private void InitPalyerData()
        {
            PlayerData playerData = new PlayerData();
            playerData.id = m_mainPlayerId;
            // TODO 这里需要读取玩家当前的载具信息
            playerData.vehicleData.id = 0;

            GameLogicManager.Instance.RegPlayerData(playerData);
        }

        private void InitGameInput()
        {
            GameInput.Create();
            GameInput.OnVkey += OnVkey;
        }

        private void OnVkey(GameVkey vkey, float arg)
        {
            GameLogicManager.Instance.InputVkey(vkey, arg, m_mainPlayerId);
        }

        #region 游戏状态控制
        /// <summary>
        /// 暂停游戏
        /// </summary>
        public void Pause()
        {
            m_pause = true;
        }

        /// <summary>
        /// 恢复游戏
        /// </summary>
        public void Resume()
        {
            m_pause = false;
        }

        /// <summary>
        /// 终止游戏
        /// </summary>
        public void Terminate()
        {
            Pause();

            if (onGameEnd != null)
            {
                onGameEnd();
            }
        }
        #endregion

        /// <summary>
        /// 创建玩家
        /// </summary>
        public void CreatePlayer()
        {
            GameMap map = GameLogicManager.Instance.GameMap;
            if (map == null)
                return;

            List<TransformData> birthPoints = GameLogicManager.Instance.GameMap.BirthPoints;

            TransformData birthPoint;
            if (birthPoints == null || birthPoints.Count == 0)
            {
                birthPoint = GameLogicManager.DEFAULT_POSITION;
            }
            else
            {
                birthPoint = birthPoints[SGFRandom.Default.Range(0, birthPoints.Count)];
            }

            GameLogicManager.Instance.CreatePlayer(m_mainPlayerId, birthPoint);
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

        /// <summary>
        /// 驱动游戏逻辑
        /// </summary>
        private void FixedUpdate()
        {
            if (m_pause)
            {
                return;
            }

            m_frameIndex++;
            GameLogicManager.Instance.EnterFrame(m_frameIndex);

            CheckTimeEnd();
        }

        private void CheckTimeEnd()
        {
            if (IsTimeLimited)
            {
                if (GetRemainTime() <= 0)
                {
                    Terminate();
                }
            }
        }

        /// <summary>
        /// 是否为限时模式
        /// </summary>
        public bool IsTimeLimited
        {
            get
            {
                return m_context.param.mode == GameMode.TimelimitPVE;
            }
        }

        /// <summary>
        /// 如果是限时模式，还剩多少时间
        /// </summary>
        /// <returns></returns>
        public int GetRemainTime()
        {
            if (IsTimeLimited)
            {
                return m_context.param.limitedTime - GetElapsedTime();
            }

            return 0;
        }

        /// <summary>
        /// 游戏经过的时间
        /// </summary>
        /// <returns></returns>
        private int GetElapsedTime()
        {
            return (int)(m_context.curFrameIndex * 0.033333333f);
        }
    }
}

