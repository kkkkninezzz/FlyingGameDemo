using Kurisu.Game;
using Kurisu.Module;
using Kurisu.Module.Pve;
using SGF.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kurisu.UI.Pve
{
    public struct FinalScore
    {
        public ulong score;

        public uint puzzle;

        public FinalScore(ulong score, uint puzzle)
        {
            this.score = score;
            this.puzzle = puzzle;
        }
    }
    public class UIPveGameFailWindow : Kurisu.Service.UIManager.UIWindow
    {
        private const int REPLA_BTN = 1;
        private const int GAME_EXIT_BTN = 2;

        private const string ScoreTextFormat = "分数：{0}";
        private const string PuzzleTextFormat = "拼图：{0}";

        public Text ScoreText;

        public Text PuzzleText;

        protected override void OnOpen(object arg = null)
        {
            base.OnOpen(arg);

            GameInput gameInput = GameInput.Instance;
            if (gameInput != null)
            {
                GameObjectUtils.SetActiveRecursively(gameInput.gameObject, false);
            }

            this.OnCloseEvent += closeArg =>
            {
                int btnIndex = (int)closeArg;
                PveModule pveModule = ModuleAPI.PveModule;
                switch (btnIndex)
                {
                    case REPLA_BTN:
                        pveModule.ReStartGame();
                        break;
                    case GAME_EXIT_BTN:
                        pveModule.ExitGame();
                        break;
                }
            };

            FinalScore finalScore = (FinalScore)arg;
            ScoreText.text = string.Format(ScoreTextFormat, finalScore.score);
            PuzzleText.text = string.Format(PuzzleTextFormat, finalScore.puzzle);
        }
        
        /// <summary>
        /// 点击重开游戏按钮
        /// </summary>
        public void OnReplayBtnClick()
        {
            this.Close(REPLA_BTN);
        }
        /// <summary>
        /// 点击游戏退出按钮
        /// </summary>
        public void OnGameExitBtnClick()
        {
            this.Close(GAME_EXIT_BTN);
        }
        
    }
}
