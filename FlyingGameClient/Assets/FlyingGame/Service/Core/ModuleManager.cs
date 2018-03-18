using System.Collections;
using System.Collections.Generic;

using SGF;
using System;

namespace Kurisu.Service.Core
{
    public class ModuleManager : ServiceModule<ModuleManager>
    {
        class MessageObject
        {
            // 消息的目标
            public string target;

            public string msg;

            public object[] args;
        }

        private Dictionary<string, BusinessModule> m_mapModules;

        private Dictionary<string, EventTable> m_mapPreListenEvents;

        private Dictionary<string, List<MessageObject>> m_mapCacheMessage;

        private string m_domain;

        private ModuleManager()
        {
            m_mapModules = new Dictionary<string, BusinessModule>();
            m_mapPreListenEvents = new Dictionary<string, EventTable>();
            m_mapCacheMessage = new Dictionary<string, List<MessageObject>>();
        }

        public void Init(string domain = ConfigConstants.DEFAULT_DOMAIN)
        {
            m_domain = domain;
        }

        public T CreateModule<T>(object args = null) where T : BusinessModule
        {
            return CreateModule(typeof(T).Name, args) as T;
        }

        public BusinessModule CreateModule(string name, object args = null)
        {
            if (m_mapModules.ContainsKey(name))
                return m_mapModules[name];

            BusinessModule module = CreateModuleInstance(name);

            InitPreListenEventsForModule(module);

            InitCacheMessageForModule(module);

            return module;
        }

        private BusinessModule CreateModuleInstance(string name)
        {
            BusinessModule module = null;

            Type type = Type.GetType(m_domain + "." + name);
            if (type != null)
            {
                module = Activator.CreateInstance(type) as BusinessModule;
            }
            else
            {
                module = new LuaModule(name);
            }

            return module;
        }

        private void InitPreListenEventsForModule(BusinessModule module)
        {
            if (m_mapPreListenEvents.ContainsKey(module.Name))
            {
                EventTable tblEvent = m_mapPreListenEvents[module.Name];
                m_mapPreListenEvents.Remove(module.Name);

                module.setEventTable(tblEvent);
            }
        }

        private void InitCacheMessageForModule(BusinessModule module)
        {
            if (m_mapCacheMessage.ContainsKey(module.Name))
            {
                List<MessageObject> list = m_mapCacheMessage[module.Name];
                if (list == null || list.Count == 0)
                    return;

                foreach (MessageObject msgObj in list)
                {
                    module.HandleMessage(msgObj.msg, msgObj.args);
                }
            }
        }

        public void ReleaseModule(BusinessModule module)
        {
            if (module == null)
                return;

            if (m_mapModules.ContainsKey(module.Name))
            {
                m_mapModules.Remove(module.Name);
                module.Release();
            }
        }

        public void ReleaseAll()
        {
            foreach (var @event in m_mapPreListenEvents)
            {
                @event.Value.Clear();
            }
            m_mapPreListenEvents.Clear();

            m_mapCacheMessage.Clear();

            foreach (var module in m_mapModules)
            {
                module.Value.Release();
            }
            m_mapModules.Clear();
        }

        //=================================================================================

        public T GetModule<T>() where T : BusinessModule
        {
            return GetModule(typeof(T).Name) as T;
        }

        public BusinessModule GetModule(string name)
        {
            if (m_mapModules.ContainsKey(name))
            {
                return m_mapModules[name];
            }

            return null;
        }

        //=================================================================================

        public void SendMessage(string target, string msg, params object[] args)
        {
            BusinessModule module = GetModule(target);
            if (module != null)
            {
                module.HandleMessage(msg, args);
            }
            else
            {
                MessageObject msgObj = new MessageObject();
                msgObj.target = target;
                msgObj.msg = msg;
                msgObj.args = args;

                GetCacheMessageList(target).Add(msgObj);
            }
        }

        private List<MessageObject> GetCacheMessageList(string target)
        {
            List<MessageObject> list = null;

            if (m_mapCacheMessage.ContainsKey(target))
            {
                list = m_mapCacheMessage[target];
            }
            else
            {
                list = new List<MessageObject>();
                m_mapCacheMessage.Add(target, list);
            }

            return list;
        }

        //=================================================================================

        public ModuleEvent Event(string target, string type)
        {
            ModuleEvent evt = null;
            BusinessModule module = GetModule(target);
            
            if (module != null)
            {
                evt = module.Event(type);
            } else
            {
                EventTable table = GetPreListenEventTable(target);
                evt = table.GetEvent(type);
            }

            return evt;
        }

        private EventTable GetPreListenEventTable(string target)
        {
            EventTable table = null;

            if (!m_mapPreListenEvents.ContainsKey(target))
            {
                table = new EventTable();
                m_mapPreListenEvents.Add(target, table);
            } else
            {
                table = m_mapPreListenEvents[target];
            }

            return table;
        }
    }
}

 