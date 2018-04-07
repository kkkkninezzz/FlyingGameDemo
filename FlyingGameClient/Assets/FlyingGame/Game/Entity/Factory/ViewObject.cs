using UnityEngine;
using System.Collections;
using System;

namespace Kurisu.Game.Entity.Factory
{
    public abstract class ViewObject : MonoBehaviour, IRecyclableObject
    {
        private string m_recycleType;

        internal void CreateInFactory(EntityObject entity, string recycleType)
        {
            m_recycleType = recycleType;

            Create(entity);
        }

        protected abstract void Create(EntityObject entity);

        internal void ReleaseInFactory()
        {
            Release();
        }

        protected abstract void Release();

        public string GetRecycleType()
        {
            return m_recycleType;
        }

        public void Dispose()
        {
            try
            {
                GameObject.Destroy(this.gameObject);
            } catch (Exception e)
            {

            }
        }
    }
}

