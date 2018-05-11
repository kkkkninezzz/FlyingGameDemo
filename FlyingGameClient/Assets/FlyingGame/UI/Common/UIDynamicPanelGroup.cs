using UnityEngine;
using System.Collections.Generic;
using SGF.Utils;

namespace Kurisu.UI.Ccommon
{
    /// <summary>
    /// 实现面板切换，该类应该添加给当前所有面板的父级结点
    /// </summary>
    public class UIDynamicPanelGroup : MonoBehaviour
    {

        private List<GameObject> m_gameObjects;

        /// <summary>
        /// 上一次激活的面板
        /// </summary>
        private int m_lastIndex = -1;

        public void Init(List<GameObject> gameObjs)
        {
            m_gameObjects = gameObjs;

            // 所有对象直接隐藏
            foreach (GameObject go in m_gameObjects)
            {
                GameObjectUtils.SetActiveRecursively(go, false);
            }
        }

        public void ActivePanel(int index)
        {
            // 相同下标则直接返回
            if (index == m_lastIndex)
            {
                return;
            }

            if (m_lastIndex >= 0)
            {
                GameObjectUtils.SetActiveRecursively(m_gameObjects[m_lastIndex], false);
            }

            m_lastIndex = index;
            GameObjectUtils.SetActiveRecursively(m_gameObjects[m_lastIndex], true);
        }
    }
}

