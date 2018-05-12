using UnityEngine;
using System.Collections;
using Kurisu.Module.Pve;
using Kurisu.Module;
using SGF.Utils;
using Kurisu.Game;

namespace Kurisu.UI.Pve
{
    public class UIPveGameWinWindow : Kurisu.Service.UIManager.UIWindow
    {
        private const int REPLA_BTN = 1;
        private const int GAME_EXIT_BTN = 2;

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
                        pveModule.ResumeGame();
                        break;
                    case GAME_EXIT_BTN:
                        pveModule.ExitGame();
                        break;
                }
            };
        }

        /// <summary>
        /// 点击返回游戏按钮
        /// </summary>
        public void OnGoBackBtnClick()
        {
            GameInput gameInput = GameInput.Instance;
            if (gameInput != null)
            {
                GameObjectUtils.SetActiveRecursively(gameInput.gameObject, true);
            }
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

