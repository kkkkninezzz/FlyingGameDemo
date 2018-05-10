using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

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

        /// <summary>
        /// 初始脚本
        /// </summary>
        /// <param name="btns"></param>
        public void Init(List<Button> btns, LabelBtnClickEvent clickEvent)
        {
            this.m_btns = btns;

            for (int i = 0; i < m_btns.Count; i++)
            {
                Button btn = m_btns[i];
                btn.onClick.AddListener(() => OnBtnClick(i));
                btn.onClick.AddListener(() => clickEvent(i));
            }
        }
         
        /// <summary>
        /// 清除按钮组的事件
        /// </summary>
        private void Clear()
        {
            if (m_btns == null ||  m_btns.Count <= 0)
            {
                return;
            }

            foreach (Button btn in m_btns)
            {
                btn.onClick.RemoveAllListeners();
            }

            m_btns = null;
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
