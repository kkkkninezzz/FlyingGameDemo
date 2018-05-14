using UnityEngine;
using System.Collections;
using Kurisu.Service.UIManager.View;

namespace Kurisu.UI.Ccommon
{
    /// <summary>
    /// 受限制的按钮
    /// </summary>
    public abstract class RestrictedButton<T> : UICommonView<T> where T : ViewModel
    {

        /// <summary>
        /// 能否被点击
        /// </summary>
        /// <returns></returns>
        public virtual bool CanClick()
        {
            return false;
        }
    }
}

