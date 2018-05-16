using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using SGF;
using Kurisu.Game;
using Kurisu.Game.Data;
using Kurisu.Game.Map;
using SGF.Unity;

namespace Kurisu.Module.Pve
{
    public class PveGame
    {

        private uint m_mainPlayerId = 1;
        private int m_frameIndex = 0;
        private bool m_pause = false;

        public event Action onMainPlayerDie;
        public event Action onGameEnd;
        public event Action onMainPlayerArriveEnd;

        private GameContext m_context;

        /// <summary>
        /// 游戏分数
        /// </summary>
        private ulong m_gameScore;

        /// <summary>
        /// 帧数的转换率，经过一帧就增加1分
        /// </summary>
        private uint m_frameConversionRate = 1;

        /// <summary>
        /// 拼图数量
        /// </summary>
        private uint m_puzzleCount;

        public void Start(GameParam param)
        {
            GameLogicManager gameManager = GameLogicManager.Instance;

            gameManager.CreateGame(param);
            gameManager.onPlayerDie += OnPlayerDie;
            gameManager.onPlayerArriveEnd += OnPlayerArriveEnd;
            m_context = gameManager.Context;

            // 初始玩家数据
            InitPalyerData();

            InitGameInput();

            // 监听frame输入
            MonoHelper.AddUpdateListener(UpdateGame);

            GameCamera.FocusPlayerId = m_mainPlayerId;
        }

        private void InitPalyerData()
        {
            PlayerData playerData = new PlayerData();
            playerData.id = m_mainPlayerId;
            // TODO 这里需要读取玩家当前的载具信息
            playerData.vehicleData.id = 0;

            GameLogicManager.Instance.RegPlayerData(playerData);
        }

        #region 初始化游戏输入
        private void InitGameInput()
        {
            GameInput.Create();
            GameInput.OnVkey += OnVkey;
        }

        private void OnVkey(GameVkey vkey, float arg)
        {
            GameLogicManager.Instance.InputVkey(vkey, arg, m_mainPlayerId);
        }
        #endregion

        /// <summary>
        /// 关闭游戏
        /// </summary>
        public void Close()
        {
            MonoHelper.RemoveUpdateListener(UpdateGame);

            GameInput.Release();

            GameLogicManager.Instance.ReleaseGame();

            m_context = null;
            onMainPlayerDie = null;
            onGameEnd = null;
            onMainPlayerArriveEnd = null;

            m_gameScore = 0;
            m_puzzleCount = 0;
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
                Pause();

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

        private void OnPlayerArriveEnd(uint playerId)
        {
            if (m_mainPlayerId != playerId)
            {
                return;
            }

            Pause();

            if (onMainPlayerArriveEnd != null)
            {
                onMainPlayerArriveEnd();
            }
            else
            {
                this.LogError("OnPlayerArriveEnd() onMainPlayerArriveEnd == null!");
            }
        }

        /// <summary>
        /// 驱动游戏逻辑
        /// </summary>
        private void UpdateGame()
        {
            if (m_pause)
            {
                return;
            }

            m_frameIndex++;
            GameLogicManager.Instance.EnterFrame(m_frameIndex);

            CheckTimeEnd();

            AutoIncreaseScore();
        }

        private void CheckTimeEnd()
        {
            if (!m_pause && IsTimeLimited)
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

        #region 游戏分数相关
        /// <summary>
        /// 自动增长分数
        /// </summary>
        private void AutoIncreaseScore()
        {
            if (m_pause)
            {
                return;
            }
            m_gameScore += m_frameConversionRate;
        }

        public void IncreaseScore(uint score)
        {
            m_gameScore += score;
        }

        public void IncreaseScore(ulong score)
        {
            m_gameScore += score;
        }

        public ulong GameScore
        {
            get
            {
                return m_gameScore;
            }
        }

        public void IncreasePuzzle(uint count)
        {
            m_puzzleCount += count;
        }

        public uint PuzzleCount
        {
            get
            {
                return m_puzzleCount;
            }
        }
        #endregion
    }
}

