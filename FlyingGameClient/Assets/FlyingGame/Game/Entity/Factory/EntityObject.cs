using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kurisu.Game.Entity.Factory
{
    public abstract class EntityObject : IRecyclableObject
    {
        private bool m_isReleased = false;

        public bool IsReleased
        {
            get
            {
                return m_isReleased;
            }
        }

        internal void InstanceInFactory()
        {
            m_isReleased = false;
        }

        internal void ReleaseInFactory()
        {
            if (!m_isReleased)
            {
                Release();
                m_isReleased = true;
            }
        }

        protected abstract void Release();

        //=========================================================================================

        public virtual Vector3 Position()
        {
            return Vector3.zero;
        }

        //=========================================================================================

        public void Dispose()
        {
            // 由系统的GC机制来处理
            // Do nothing
        }

        public string GetRecycleType()
        {
            return this.GetType().FullName;
        }

        //=========================================================================================

    }
}

