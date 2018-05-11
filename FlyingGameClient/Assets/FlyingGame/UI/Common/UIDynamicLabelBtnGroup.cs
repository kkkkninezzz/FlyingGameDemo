using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using SGF;

namespace Kurisu.UI.Ccommon
{
    /// <summary>
    /// 标签按钮的点击事件
    /// </summary>
    /// <param name="index"></param>
    public delegate void LabelBtnClickEvent(int index);

    /// <summary>
    /// 处理按钮动态添加的时的按钮组
    /// </summary>
    public class UIDynamicLabelBtnGroup : MonoBehaviour
    {
        /// <summary>
        /// 保存按钮组
        /// </summary>
        private List<Button> m_btns;

        /// <summary>
        /// 选中时的颜色
        /// </summary>
        //[SerializeField]
        private Color SelectedColor = UIDef.SelectedColorForBtn;

        /// <summary>
        /// 上次点击的按钮的下标
        /// </summary>
        private int m_lastBtnIndex = -1;

        /// <summary>
        /// 按钮之前的颜色
        /// </summary>
        private Color m_lastBtnColor;

        private List<UnityAction> m_changeBtnEvents = new List<UnityAction>();

        private List<UnityAction> m_extraClickEvents = new List<UnityAction>();

        /// <summary>
        /// 初始脚本
        /// </summary>
        /// <param name="btns"></param>
        public void Init(List<Button> btns, LabelBtnClickEvent clickEvent = null)
        {
            Clear();
            this.m_btns = btns;

            for (int i = 0; i < m_btns.Count; i++)
            {
                Button btn = m_btns[i];

                UnityAction changeBtnEvent = GetChangeBtnEvent(i);
                btn.onClick.AddListener(changeBtnEvent);
                m_changeBtnEvents.Add(changeBtnEvent);

                if (clickEvent != null)
                {
                    UnityAction extraClickEvent = GetExtraClickEvent(clickEvent, i);
                    btn.onClick.AddListener(extraClickEvent);
                    m_extraClickEvents.Add(extraClickEvent);
                }
            }
        }
        
        private UnityAction GetChangeBtnEvent(int index)
        {
            return () => OnBtnClick(index);
        }

        private UnityAction GetExtraClickEvent(LabelBtnClickEvent clickEvent, int index)
        {
            return () => clickEvent(index);
        }

        /// <summary>
        /// 清除按钮组的事件，这里只会清除通过该脚本添加的事件
        /// </summary>
        private void Clear()
        {
            if (m_btns == null ||  m_btns.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < m_btns.Count; i++)
            {
                Button btn = m_btns[i];
                btn.onClick.RemoveListener(m_changeBtnEvents[i]);
                if (m_extraClickEvents.Count > 0)
                {
                    btn.onClick.RemoveListener(m_extraClickEvents[i]);
                }
            }

            m_btns = null;
            m_changeBtnEvents.Clear();
            m_extraClickEvents.Clear();
        }

        /// <summary>
        /// 点击第一个按钮
        /// </summary>
        public void ClickFirstBtn()
        {
            if (m_btns == null || m_btns.Count <= 0)
            {
                return;
            }

            m_btns[0].onClick.Invoke();
        }

        private void OnBtnClick(int index)
        {
            // this.Log("OnBtnClick() : index = {0}", index);
            if (index == m_lastBtnIndex)
            {
                return;
            }

            Button lastBtn;
            if (m_lastBtnIndex >= 0)
            {
                // 恢复按钮的颜色
                lastBtn = m_btns[m_lastBtnIndex];
                ChangeBtnColor(lastBtn, m_lastBtnColor);
            }

            // 将选中的按钮颜色更改
            m_lastBtnIndex = index;
            lastBtn = m_btns[m_lastBtnIndex];
            m_lastBtnColor = lastBtn.colors.normalColor;
            ChangeBtnColor(lastBtn, SelectedColor);

        }

        private void ChangeBtnColor(Button btn, Color color)
        {
            ColorBlock cb = btn.colors;
            cb.normalColor = color;
            cb.highlightedColor = color;
            btn.colors = cb;
        }
    }
}
