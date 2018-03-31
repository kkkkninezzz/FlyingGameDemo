using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using SGF;

namespace Kurisu.Service.UIManager
{
    public class UIWindow : UIPanel
    {
        //===========================================================
        public delegate void CloseEvent(object arg = null);
        //===========================================================

        /// <summary>
        /// 关闭按钮，大部分窗口都会有关闭按钮
        /// </summary>
        [SerializeField]
        protected Button m_closeBtn;

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        public event CloseEvent OnCloseEvent;

        /// <summary>
        /// 打开UI的参数
        /// </summary>
        protected object m_openArg;

        /// <summary>
        /// 该UI的当前实例是否曾经被打开过
        /// </summary>
        protected bool m_isOpenedOnce;

        /// <summary>
        /// 当UI可用时调用
        /// </summary>
        protected void OnEnable()
        {
            this.Log("OnEnable() ");
            if (m_closeBtn != null)
            {
                m_closeBtn.onClick.AddListener(OnBtnClose);
            }
#if UNITY_EDITOR
            if (m_isOpenedOnce)
            {
                // 如果UI曾经被打开过
                // 则可以通过UnityEditor来快速触发Open/Close操作
                // 方便调试
                OnOpen(m_openArg);
            }
#endif
        }

        /// <summary>
        /// 当UI不可用时调用
        /// </summary>
        protected void OnDisable()
        {
            this.Log("OnDisable() ");
            /*
#if UNITY_EDITOR
            if (m_isOpenedOnce)
            {
                // 如果UI曾经被打开过
                // 则可以通过UnityEditor来快速触发Open/Close操作
                // 方便调试
                OnClose();
                if (OnCloseEvent != null)
                {
                    OnCloseEvent();
                    OnCloseEvent = null;
                }
            }
#endif
*/
            if (m_closeBtn != null)
            {
                m_closeBtn.onClick.RemoveAllListeners();
            }
        }

        /// <summary>
        /// 当点击关闭按钮时调用
        /// 但是并不是每一个Window都有关闭按钮
        /// </summary>
        private void OnBtnClose()
        {
            this.Log("OnBtnClose() ");
            Close();
        }

        /// <summary>
        /// 调用它打开UIWindow
        /// </summary>
        /// <param name="arg"></param>
        public sealed override void Open(object arg = null)
        {
            base.Open(arg);

            m_openArg = arg;
            m_isOpenedOnce = false;

            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }

            OnOpen(arg);
            m_isOpenedOnce = true;
        }

        /// <summary>
        /// 调用它关闭UIWindow
        /// </summary>
        public sealed override void Close(object arg = null)
        {
            base.Close(arg);

            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }

            OnClose(arg);
            if (OnCloseEvent != null)
            {
                OnCloseEvent(arg);
                OnCloseEvent = null;
            }
        }
    }
}

