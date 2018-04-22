using SGF;
using System.Collections;
using System.Reflection;

namespace Kurisu.Service.Core
{
    public abstract class BusinessModule : Module
    {
        private string m_name = "";

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(m_name))
                    m_name = this.GetType().Name;

                return m_name;
            }
        }

        public string Title;

        public BusinessModule()
        {

        }

        internal BusinessModule(string name)
        {
            m_name = name;
        }

        private EventTable m_tblEvent;

        public ModuleEvent Event(string type)
        {
            return GetEventTable().GetEvent(type);
        }

        internal void setEventTable(EventTable tblEvent)
        {
            m_tblEvent = tblEvent;
        }

        protected EventTable GetEventTable()
        {
            if (m_tblEvent == null)
                m_tblEvent = new EventTable();

            return m_tblEvent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">调用模块的方法名</param>
        /// <param name="args"></param>
        internal void HandleMessage(string message, object[] args)
        {
            this.Log("HandleMessage() msg = {0}, args = {1}", message, args);

            MethodInfo mi = this.GetType().GetMethod(message, BindingFlags.NonPublic | BindingFlags.Instance);

            if (mi != null)
            {
                mi.Invoke(this, BindingFlags.NonPublic, null, args, null);
            } else
            {
                OnModuleMessage(message, args);
            }
        }

        protected virtual void OnModuleMessage(string msg, object[] args)
        {
            this.Log("OnModuleMessage() msg = {0}, args = {1}", msg, args);
        }

        public virtual void Create(object args = null)
        {
            this.Log("Create() args = {0}", args);
        }

        public override void Release()
        {
            if (m_tblEvent != null)
            { 
                m_tblEvent.Clear();
                m_tblEvent = null;
            }
            base.Release();
        }

        protected virtual void Show(object arg)
        {
            this.Log("Show() arg:{0}", arg);
        }
    }
}

