using UnityEngine;
using System.Collections;

using Kurisu.Service.UIManager;
using Kurisu.UI.Ccommon;
namespace Kurisu.UI
{
    public static class UIAPI
    {

        /// <summary>
        /// 打开一个MsgBox
        /// </summary>
        /// <param name="title">MsgBox的标题</param>
        /// <param name="content">MsgBox的内容</param>
        /// <param name="btnText">MagBox的按钮内容，格式为 "确定|取消"</param>
        /// <param name="onCloseEvent">当按钮关闭时调用的事件</param>
        /// <returns></returns>
        public static UIWindow ShowMsgBox(string title, string content, string btnText, UIWindow.CloseEvent onCloseEvent = null)
        {
            UIMsgBox.UIMsgBoxArg arg = new UIMsgBox.UIMsgBoxArg();
            arg.content = content;
            arg.title = title;
            arg.btnText = btnText;

            UIWindow wnd = UIManager.Instance.OpenWindow(UIDef.UIMsgBox, arg);

            if (wnd != null && onCloseEvent != null)
            {
                wnd.OnCloseEvent += closeArg =>
                {
                    onCloseEvent(closeArg);
                };
            }

            return wnd;
        }

        /// <summary>
        /// 打开一个不带标题的MsgBox
        /// </summary>
        /// <param name="content">MsgBox的内容</param>
        /// <param name="btnText">MagBox的按钮内容，格式为 "确定|取消"</param>
        /// <param name="onCloseEvent">当按钮关闭时调用的事件</param>
        /// <returns></returns>
        public static UIWindow ShowMsgBox(string content, string btnText, UIWindow.CloseEvent onCloseEvent = null)
        {
            return ShowMsgBox(string.Empty, content, btnText, onCloseEvent);
        }
    }
}

