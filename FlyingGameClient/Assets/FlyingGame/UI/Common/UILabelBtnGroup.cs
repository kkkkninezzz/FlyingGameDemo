using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Kurisu.UI.Ccommon
{
    /// <summary>
    /// 用来实现按钮点击后切换
    /// </summary>
    public class UILabelBtnGroup : MonoBehaviour
    {
        [SerializeField]
        private List<Button> btns;

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

        public void OnBtnClick(int index)
        {
            if (index == m_lastBtnIndex)
            {
                return;
            }

            Button lastBtn;
            if (m_lastBtnIndex >= 0)
            {
                // 恢复按钮的颜色
                lastBtn = btns[m_lastBtnIndex];
                ChangeBtnColor(lastBtn, m_lastBtnColor);
            }

            // 将选中的按钮颜色更改
            m_lastBtnIndex = index;
            lastBtn = btns[m_lastBtnIndex];
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

