using System.Collections;

namespace Kurisu.Service.UIManager.View
{
    /// <summary>
    /// 视图模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ViewModel
    {
        private bool m_isDataChanged = false;

        public bool IsDataChanged
        {
            get
            {
                return m_isDataChanged;
            }
        }

        /// <summary>
        /// 强制表示数据已发生变动
        /// </summary>
        public void SetDataChanged()
        {
            m_isDataChanged = true;
        }

        /// <summary>
        /// 消耗这次数据变动
        /// </summary>
        public void CostDataChanged()
        {
            m_isDataChanged = false;
        }
    }
}

