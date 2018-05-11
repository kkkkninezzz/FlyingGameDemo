
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
        public static Kurisu.Service.UIManager.UIWindow ShowMsgBox(string title, string content, string btnText, Kurisu.Service.UIManager.UIWindow.CloseEvent onCloseEvent = null)
        {
            UIMsgBox.UIMsgBoxArg arg = new UIMsgBox.UIMsgBoxArg();
            arg.content = content;
            arg.title = title;
            arg.btnText = btnText;

            Kurisu.Service.UIManager.UIWindow wnd = UIManager.Instance.OpenWindow(UIDef.UIMsgBox, arg);

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
        public static Kurisu.Service.UIManager.UIWindow ShowMsgBox(string content, string btnText, Kurisu.Service.UIManager.UIWindow.CloseEvent onCloseEvent = null)
        {
            return ShowMsgBox(string.Empty, content, btnText, onCloseEvent);
        }

        /// <summary>
        /// 在指定场景打开UIPage
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="page"></param>
        /// <param name="arg"></param>
        public static void ShowUIPage(string scene, string page, object arg = null)
        {
            UIManager.Instance.OpenPage(scene, page, arg);
        }

        /// <summary>
        /// 在主场景打开UIPage
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="page"></param>
        /// <param name="arg"></param>
        public static void ShowUIPage(string page, object arg = null)
        {
            UIManager.Instance.OpenPage(page, arg);
        }

        /// <summary>
        /// 在主场景打开一个UIWindow
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Kurisu.Service.UIManager.UIWindow ShowUIWindow(string name, object arg = null)
        {
            return UIManager.Instance.OpenWindow(name, arg);
        }
        

    }
}

