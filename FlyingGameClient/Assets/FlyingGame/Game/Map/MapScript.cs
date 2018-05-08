using UnityEngine;
using System.Collections.Generic;
using Kurisu.Game.Data;

namespace Kurisu.Game.Map
{
    public interface MapScript
    {
        /// <summary>
        /// 第一次加载
        /// </summary>
        void FirstLoad();

        void EnterFrame(int frameIndex);
    }

    public abstract class AbstractMapScript<T> : MapScript
    {
        protected T m_data;
        protected Transform m_container;

        public AbstractMapScript(T data, Transform container)
        {
            m_data = data;
            m_container = container;
        }

        public abstract void FirstLoad();
        public abstract void EnterFrame(int frameIndex);
    }
}

