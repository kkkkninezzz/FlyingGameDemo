using UnityEngine;
using System.Collections;

using SGF;

namespace Kurisu.Service.UIManager
{
    public abstract class UIPanel : MonoBehaviour
    {
        /// <summary>
        /// Open() 是对应UI框架的实现
        /// </summary>
        /// <param name="arg"></param>
        public virtual void Open(object arg = null)
        {
            this.Log("Open() arg: {0}", arg);
        }

        public virtual void Close(object arg = null)
        {
            this.Log("Close() arg: {0}", arg);
        }

        /// <summary>
        /// 当前UI是否打开
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return this.gameObject.activeSelf;
            }
        }

        /// <summary>
        /// 当UI关闭时，会响应这个函数
        /// 该函数在重写时，需要支持重复调用
        /// </summary>
        public virtual void OnClose(object arg = null)
        {
            this.Log("OnClose() arg: {0}", arg);
        }

        /// <summary>
        /// 当UI打开时，会响应这个函数
        /// </summary>
        /// <param name="arg"></param>
        public virtual void OnOpen(object arg = null)
        {
            this.Log("OnOpen() arg: {0}", arg);
        }
    }
}

