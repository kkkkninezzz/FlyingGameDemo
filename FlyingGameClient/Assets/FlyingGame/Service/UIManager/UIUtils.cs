using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using SGF.Utils;

namespace Kurisu.Service.UIManager
{
    /// <summary>
    /// 为UI操作提供基础封装，使UI操作更方便
    /// </summary>
    public class UIUtils
    {
        /// <summary>
        /// 设置一个UI元素是否可见
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="value"></param>
        public static void SetActive(UIBehaviour ui, bool value)
        {
            if (ui != null && ui.gameObject != null)
                GameObjectUtils.SetActiveRecursively(ui.gameObject, value);
        }
    }

}
