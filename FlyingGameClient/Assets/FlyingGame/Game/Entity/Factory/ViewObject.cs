using UnityEngine;
using System.Collections;
using System;

namespace Kurisu.Game.Entity.Factory
{
    public abstract class ViewObject : MonoBehaviour, IRecyclableObject
    {
        private string m_recycleType;

        //===========================================================================================

        /// <summary>
        /// 在ViewFactory中创建ViewObject
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="recycleType">有些view的类型是资源名，有些则是类名</param>
        internal void CreateInFactory(EntityObject entity, string recycleType)
        {
            m_recycleType = recycleType;

            Create(entity);

            entity.View = this;
        }

        protected abstract void Create(EntityObject entity);

        //===========================================================================================

        internal void ReleaseInFactory()
        {
            Release();
        }

        protected abstract void Release();

        //===========================================================================================

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

