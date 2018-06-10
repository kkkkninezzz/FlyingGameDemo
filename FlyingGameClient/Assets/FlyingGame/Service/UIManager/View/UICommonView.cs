using UnityEngine;
using System.Collections;


namespace Kurisu.Service.UIManager.View
{
    /// <summary>
    /// 普通UI的视图，用于与数据绑定更新
    /// </summary>
    public abstract class UICommonView<T> : MonoBehaviour where T : ViewModel
    {
        protected T m_model;

        private bool m_isModelChanged;

        /// <summary>
        /// 设置Model以后，必定会更新一次视图
        /// </summary>
        /// <param name="viewModel"></param>
        public void SetModel(T viewModel)
        {
            m_model = viewModel;
            m_isModelChanged = true;

            
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (m_isModelChanged)
            {
                UpdateView();
                m_isModelChanged = false;
            }
            else if (m_model != null && m_model.IsDataChanged)
            {
                UpdateView();
                m_model.CostDataChanged();
            }
            
        }

        /// <summary>
        /// 子类View只需要实现这个方法即可，就可以在model改变时在一帧的时候去更新视图
        /// </summary>
        /// <param name="model"></param>
        protected abstract void UpdateView();
    }
}

