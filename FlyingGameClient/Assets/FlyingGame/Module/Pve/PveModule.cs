using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Kurisu.Service.Core;
using Kurisu.Game.Data;
using SGF;
using Kurisu.UI;
using Kurisu.Service.UIManager;

namespace Kurisu.Module.Pve
{
    public class PveModule : BusinessModule
    {
        private PveGame m_game;

        private GameParam m_lastGameParam;

        //显示模块的主UI
        protected override void Show(object arg)
        {
            base.Show(arg);

            UIAPI.ShowUIPage(UIDef.UIChapterPage);
        }

        public void StartGame(GameMode mode, MapData mapData)
        {
            GameParam param = new GameParam();
            param.mode = mode;
            param.mapData = mapData;
            param.limitedTime = 100;

            // 保存起来用来重开游戏
            m_lastGameParam = param;

            StartGame(param);

            // 打开战斗UI
            UIAPI.ShowUIPage(UIDef.UIPveGamePage);
        }

        private void StartGame(GameParam gameParam)
        {
            m_game = new PveGame();
            m_game.Start(gameParam);

            m_game.onGameEnd += () =>
            {
                CloseGame();
            };

            m_game.onMainPlayerArriveEnd += () =>
            {
                m_game.Pause();
                // TODO 根据不同模式有不同的结算
                UIAPI.ShowUIWindow(UIDef.UIPveGameWinWindow);
            };

            m_game.onMainPlayerDie += () =>
            {
                m_game.Pause();
                this.Log("玩家死亡");
                // TODO 根据不同模式有不同的结算
                UIAPI.ShowUIWindow(UIDef.UIPveGameFailWindow);
            };

            // 创建玩家
            m_game.CreatePlayer();
            // 手动暂停游戏，等玩家点击ready
            //m_game.Pause();
        }

        private void CloseGame()
        {
            if (m_game != null)
            {
                m_game.Close();
                m_game = null;
            }
        }


        public PveGame CurGame
        {
            get
            {
                return m_game;
            }
        }

        /// <summary>
        /// 暂停游戏
        /// </summary>
        public void PauseGame()
        {
            if (m_game == null)
            {
                return;
            }

            m_game.Pause();
            UIAPI.ShowUIWindow(UIDef.UIPveGamePauseWindow);
        }

        /// <summary>
        /// 恢复游戏
        /// </summary>
        public void ResumeGame()
        {
            if (m_game == null)
            {
                return;
            }

            m_game.Resume();
        }

        /// <summary>
        /// 退出游戏并返回到上一个界面
        /// </summary>
        public void ExitGame()
        {
            if (m_game == null)
            {
                return;
            }
            m_game.Terminate();
            m_lastGameParam = null;

            // 退出游戏以后返回到上一个页面
            UIManager.Instance.GoBackPage();
        }

        /// <summary>
        /// 中断游戏，会直接令游戏结束
        /// </summary>
        public void TerminateGame()
        {
            if (m_game != null)
            {
                m_game.Terminate();
            }
        }

        /// <summary>
        /// 重开游戏
        /// </summary>
        public void ReStartGame()
        {
            if (m_game != null)
            {
                m_game.Terminate();
            }
            StartGame(m_lastGameParam);
        }
    }
}

