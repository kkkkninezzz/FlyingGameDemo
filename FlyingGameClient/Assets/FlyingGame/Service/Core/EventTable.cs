using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Kurisu.Service.Core
{
    public class ModuleEvent : UnityEvent<object>
    {

    }

    public class ModuleEvent<T> : UnityEvent<T>
    {

    }

    public class EventTable
    {
        private Dictionary<string, ModuleEvent> m_mapEvents;

        /// <summary>
        /// 获取type对应的一个ModuleEvent
        /// 如果不存在，则实例化一个ModuleEvent
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ModuleEvent GetEvent(string type)
        {
            if (m_mapEvents == null)
                m_mapEvents = new Dictionary<string, ModuleEvent>();

            if (!m_mapEvents.ContainsKey(type))
                m_mapEvents.Add(type, new ModuleEvent());

            return m_mapEvents[type];
        }

        public void Clear()
        {
            if (m_mapEvents == null)
                return;

            foreach (var @event in m_mapEvents)
            {
                @event.Value.RemoveAllListeners();
            }

            m_mapEvents.Clear();
        }
    }
}


