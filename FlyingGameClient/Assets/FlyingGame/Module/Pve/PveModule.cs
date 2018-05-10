using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Kurisu.Service.Core;
using Kurisu.Game.Data;
using SGF;
using Kurisu.UI;

namespace Kurisu.Module.Pve
{
    public class PveModule : BusinessModule
    {
        private PveGame m_game;

        //显示模块的主UI
        protected override void Show(object arg)
        {
            base.Show(arg);

            //int mode = (int)arg;

            UIAPI.ShowUIPage(UIDef.UIChapterPage);

            //TODO 显示关卡选择UI

            //先直接启动游戏
            //StartGame((GameMode)mode);
        }

        public void StartGame(GameMode mode, MapData mapData)
        {
            GameParam param = new GameParam();
            param.mode = mode;
            param.mapData = mapData;
            param.limitedTime = 100;

            m_game = new PveGame();
            m_game.Start(param);

            m_game.onGameEnd += () =>
            {
                CloseGame();
            };

            m_game.onMainPlayerArriveEnd += () =>
            {
                CloseGame();
                // TODO 根据不同模式有不同的结算
            };

            m_game.onMainPlayerDie += () =>
            {
                //CloseGame();
                this.Log("玩家死亡");
                // TODO 根据不同模式有不同的结算
            };
            // TODO 打开战斗UI
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
    }
}

