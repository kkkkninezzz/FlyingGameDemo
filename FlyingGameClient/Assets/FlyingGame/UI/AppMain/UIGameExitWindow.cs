using UnityEngine;
using System.Collections;

namespace Kurisu.UI.AppMain
{
    public class UIGameExitWindow : Kurisu.Service.UIManager.UIWindow
    {
        private const int CANCEL_BTN = 1;
        private const int GAME_EXIT_BTN = 2;

        protected override void OnOpen(object arg = null)
        {
            base.OnOpen(arg);

            this.OnCloseEvent += closeArg =>
            {
                int btnIndex = (int)closeArg;
                switch (btnIndex)
                {
                    case GAME_EXIT_BTN:
                        Debug.Log("quit");
                        Application.Quit();
                        break;
                }
            };

           
        }

        public void OnCancelBtnClick()
        {
            this.Close(CANCEL_BTN);
        }

        public void OnGameExitBtnClick()
        {
            this.Close(GAME_EXIT_BTN);
        }
    }

}
