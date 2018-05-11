using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Kurisu.Game;
using SGF.Utils;

namespace Kurisu.UI.Pve
{
    /// <summary>
    /// 游戏过程中暂停的UI
    /// </summary>
    public class UIPveGamePauseWindow : Kurisu.Service.UIManager.UIWindow
    {
        protected override void OnOpen(object arg = null)
        {
            base.OnOpen(arg);

            GameInput gameInput = GameInput.Instance;
            if (gameInput != null)
            {
                GameObjectUtils.SetActiveRecursively(gameInput.gameObject, false);
            }
        }

        protected override void OnClose(object arg = null)
        {
            base.OnClose(arg);

            GameInput gameInput = GameInput.Instance;
            if (gameInput != null)
            {
                GameObjectUtils.SetActiveRecursively(gameInput.gameObject, true);
            }
        }

        /// <summary>
        /// 点击返回游戏按钮
        /// </summary>
        public void OnGoBackBtnClick()
        {

        }
        /// <summary>
        /// 点击游戏退出按钮
        /// </summary>
        public void OnGameExitBtnClick()
        {

        }
    }
}
