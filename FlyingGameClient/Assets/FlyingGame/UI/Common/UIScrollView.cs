using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Kurisu.UI.Ccommon
{
    /// <summary>
    /// 滚动视图
    /// </summary>
    [RequireComponent(typeof(ScrollRect))]
    public class UIScrollView : MonoBehaviour
    {
        /// <summary>
        /// 滚动视图的内容区域
        /// </summary>
        public GameObject Content;

        private List<GameObject> m_children = new List<GameObject>();

        public void Start()
        {
            if (Content == null)
            {
                Content = transform.Find("Content").gameObject;
            }
        }

        /// <summary>
        /// 将给定的游戏对象添加到滚动视图的内容区域
        /// </summary>
        /// <param name="children"></param>
        public void AddChildren(List<GameObject> children)
        {
            if (children == null || children.Count <= 0)
            {
                return;
            }
            
            foreach (GameObject child in children)
            {
                child.transform.parent = Content.transform;
                m_children.Add(child);
            }
            
        }

        /// <summary>
        /// 将给定的游戏对象添加到滚动视图的内容区域
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(GameObject child)
        {
            if (child == null)
            {
                return;
            }
            
            child.transform.SetParent(Content.transform);
            m_children.Add(child);
        }

        /// <summary>
        /// 清除该滚动视图下的子对象，这里仅清除由AddChildren或者AddChild方法添加的的
        /// </summary>
        public void Clear()
        {
            if (m_children == null || m_children.Count <= 0)
            {
                return;
            }

            for(int i = 0; i < m_children.Count; i++)
            {
                Destroy(m_children[i]);
            }

            m_children.Clear();
        }
    }
}
