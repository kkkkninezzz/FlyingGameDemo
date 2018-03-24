using UnityEngine;
using System.Collections;

namespace Kurisu.Service.UIManager
{
    public class UIWidget : UIPanel
    {
        /// <summary>
        /// 打开UI的参数
        /// </summary>
        protected object m_openArg;

        /// <summary>
        /// 调用它打开UI
        /// </summary>
        /// <param name="arg"></param>
        public sealed override void Open(object arg = null)
        {
            base.Open(arg);

            m_openArg = arg;

            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }

            OnOpen(arg);
        }

        /// <summary>
        /// 调用它关闭UI
        /// </summary>
        public sealed override void Close()
        {
            base.Close();

            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }

            OnClose();
        }
    }
}

