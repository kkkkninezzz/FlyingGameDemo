using System;
using System.Collections.Generic;

namespace Kurisu.Game.Entity.Factory
{
    public interface IRecyclableObject
    {
        /// <summary>
        /// 获取回收类型
        /// </summary>
        /// <returns></returns>
        string GetRecycleType();

        /// <summary>
        /// 真正的去销毁对象
        /// </summary>
        void Dispose();
    }

    public class Recycler
    {
        /// <summary>
        /// 被工厂回收的空闲对象列表
        /// </summary>
        private static Dictionary<string, Stack<IRecyclableObject>> m_idleObjPool;

        public Recycler()
        {
            m_idleObjPool = new Dictionary<string, Stack<IRecyclableObject>>();
        }

        public void Release()
        {
            foreach (var pair in m_idleObjPool)
            {
                foreach (var obj in pair.Value)
                {
                    obj.Dispose();
                }

                pair.Value.Clear();
            }

        }

        public void Push(IRecyclableObject obj)
        {
            if (obj == null)
                return;

            string type = obj.GetRecycleType();
            Stack<IRecyclableObject> idleObjStack = m_idleObjPool[type];

            if (idleObjStack == null)
            {
                idleObjStack = new Stack<IRecyclableObject>();
                m_idleObjPool.Add(type, idleObjStack);
            }

            idleObjStack.Push(obj);
        }

        public IRecyclableObject Pop(string type)
        {
            Stack<IRecyclableObject> idleObjStack = m_idleObjPool[type];

            if (idleObjStack != null && idleObjStack.Count > 0)
            {
                return idleObjStack.Pop();
            }

            return null;
        }

    }
}

