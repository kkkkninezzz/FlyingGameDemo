using UnityEngine;
using System.Collections;

using SGF;

namespace Kurisu.Service.UIManager.Example
{
    public class UIPage2 : UIPage
    {

        public void OnOpenWindow1Btn()
        {
            UIManager.Instance.OpenWindow<UIWindow1>().OnCloseEvent += OnWindow1Close;
        }

        private void OnWindow1Close(object arg = null)
        {
            this.Log("OnWindow1Close() ");

        }

        public void OnOpenWidget1Btn()
        {
            UIManager.Instance.OpenWidget("UIWidget1");
        }
    }
}

