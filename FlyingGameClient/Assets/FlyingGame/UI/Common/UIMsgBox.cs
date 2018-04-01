using Kurisu.Service.UIManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using SGF;

namespace Kurisu.UI.Ccommon
{
    public class UIMsgBox : UIWindow
    {
        public class UIMsgBoxArg
        {
            public string title = "";
            public string content = "";
            public string btnText;  // 例如："确定|取消|关闭"
        }

        private UIMsgBoxArg m_arg;
        public Text textContent;
        public UIBehaviour ctlTitle;
        public Button[] buttons;

        protected override void OnOpen(object arg = null)
        {
            base.OnOpen(arg);
            m_arg = arg as UIMsgBoxArg;
            if (m_arg == null)
            {
                this.LogError("OnOpen() arg must be not null!");
                return;
            }

            textContent.text = m_arg.content;
            string[] btnTexts = m_arg.btnText.Split('|');

            UIUtils.SetChildText(ctlTitle, m_arg.title);
            UIUtils.SetActive(ctlTitle, !string.IsNullOrEmpty(m_arg.title));

            float btnWidth = 200;
            float btnStartX = (1 - btnTexts.Length) * btnWidth / 2;

            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < btnTexts.Length)
                {
                    UIUtils.SetButtonText(buttons[i], btnTexts[i]);
                    UIUtils.SetActive(buttons[i], true);
                    Vector3 pos = buttons[i].transform.localPosition;
                    pos.x = btnStartX + i * btnWidth;
                    buttons[i].transform.localPosition = pos;
                }
                else
                {
                    UIUtils.SetActive(buttons[i], false);
                }
            }
        }

        public void OnBtnClick(int btnIndex)
        {
            this.Close(btnIndex);
        }
        
    }
}

