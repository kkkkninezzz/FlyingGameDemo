using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SGF.Utils;

namespace Kurisu.Service.UIManager
{
    /// <summary>
    /// 为UI操作提供基础封装，使UI操作更方便
    /// </summary>
    public static class UIUtils
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

        /// <summary>
        /// 为ui添加一个文本对象
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="text"></param>
        public static void SetChildText(UIBehaviour ui, string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            GameObject textUI = new GameObject("title", typeof(Text));
            textUI.transform.parent = ui.transform;
        }
    }

}
