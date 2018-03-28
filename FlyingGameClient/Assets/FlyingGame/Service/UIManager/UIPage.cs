using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using SGF;

namespace Kurisu.Service.UIManager
{
    public class UIPage : UIPanel
    {
        /// <summary>
        /// 返回按钮，大部分Page都会有返回按钮
        /// </summary>
        [SerializeField]
        protected Button m_goBackBtn;

        /// <summary>
        /// 打开UI的参数
        /// </summary>
        protected object m_openArg;

        /// <summary>
        /// 该UI的当前实例是否曾经被打开过
        /// </summary>
        protected bool m_isOpenedOnce;

        /// <summary>
        /// 当UIPage被激活时调用
        /// </summary>
        protected void OnEnable()
        {
            this.Log("OnEnable() ");
            if (m_goBackBtn != null)
            {
                m_goBackBtn.onClick.AddListener(OnGoBackBtn);
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
#if UNITY_EDITOR
            if (m_isOpenedOnce)
            {
                // 如果UI曾经被打开过
                // 则可以通过UnityEditor来快速触发Open/Close操作
                // 方便调试
                OnClose();
            }
#endif

            if (m_goBackBtn != null)
            {
                m_goBackBtn.onClick.RemoveAllListeners();
            }
        }

        /// <summary>
        /// 当点击“返回”时调用
        /// 但是并不是每一个Page都有返回按钮
        /// </summary>
        private void OnGoBackBtn()
        {
            this.Log("OnGoBackBtn() ");
            UIManager.Instance.GoBackPage();
        }

        /// <summary>
        /// 调用它打开UIPage
        /// </summary>
        /// <param name="arg"></param>
        public override void Open(object arg = null)
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
        /// 调用它关闭UI
        /// </summary>
        public override void Close(object arg = null)
        {
            base.Close();

            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }

            OnClose(arg);
        }
    }
}

