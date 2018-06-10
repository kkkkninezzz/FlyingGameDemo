using Kurisu.Service.UIManager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kurisu.UI.Help
{
    public class UIHelpPage : UIPage
    {
        [SerializeField]
        private Button GameBackgroundBtn;

        [SerializeField]
        private GameObject GameBackgroundPanel;

        [SerializeField]
        private Button OperationBtn;

        [SerializeField]
        private GameObject OperationPanel;

        private bool isFirstOpen = false;

        protected override void OnOpen(object arg = null)
        {
            // 第一次打开时去主动调用一次ChapterModeBtn的click方法
            if (!isFirstOpen)
            {
                GameBackgroundBtn.onClick.Invoke();
                isFirstOpen = true;
            }

        }

        public void OnGameBackgroundBtnClick()
        {
            GameBackgroundPanel.SetActive(true);
            OperationPanel.SetActive(false);
        }

        public void OnOperationBtnClick()
        {
            GameBackgroundPanel.SetActive(false);
            OperationPanel.SetActive(true);
        }
    }
}

