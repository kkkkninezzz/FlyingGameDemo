using UnityEngine;
using System.Collections;
using SGF;

namespace Kurisu.Service.Core.Example
{
    class ModuleA : BusinessModule
    {
        public override void Create(object args = null)
        {
            base.Create(args);

            // 业务层模块之间，通过Message进行通讯
            ModuleManager.Intstance.SendMessage("ModuleB", "MessageFromA_1", 1, 2, 3);
            ModuleManager.Intstance.SendMessage("ModuleB", "MessageFromA_2", "abc", 123);

            // 业务模块之间，通过Event进行通讯
            ModuleManager.Intstance.Event("ModuleB", "OnModuleEventB").AddListener(OnModuleEventB);

            // 业务层调用服务层，通过事件监听回调
            ModuleC.Intstance.OnEvent.AddListener(OnModuleEventC);
            ModuleC.Intstance.DoSomething();

            // 全局事件
            GlobalEvent.OnLogin.AddListener(OnLogin);
        }

        private void OnModuleEventC(object args)
        {
            this.Log("OnModuleEventC() args = {0}", args);
        }

        private void OnModuleEventB(object args)
        {
            this.Log("OnModuleEventB() args = {0}", args);
        }

        private void OnLogin(bool args)
        {
            this.Log("OnLogin() args = {0}", args);
        }
    }

    class ModuleB : BusinessModule
    {
        public ModuleEvent OnModuleEventB
        {
            get
            {
                return Event("OnModuleEventB");
            }
        }

        public override void Create(object args = null)
        {
            base.Create(args);

            OnModuleEventB.Invoke("aaaa");
        }

        protected void MessageFromA_2(string args1, int args2)
        {
            this.Log("MessageFromA_2() args:{0}, {1}", args1, args2);
        }

        protected override void OnModuleMessage(string msg, object[] args)
        {
            base.OnModuleMessage(msg, args);

            this.Log("OnModuleMessage() msg: {0}, args: {1}, {2}, {3}", msg, args[0], args[1], args[2]);
        }
    }

    class ModuleC : ServiceModule<ModuleC>
    {
        public ModuleEvent OnEvent = new ModuleEvent();

        private ModuleC()
        {

        }

        public void Init()
        {

        }

        public void DoSomething()
        {
            OnEvent.Invoke(null);
        }
    }

    public class Example
    {
        public void Init()
        {
            ModuleC.Intstance.Init();
            ModuleManager.Intstance.Init("Kurisu.Service.Core.Example");

            ModuleManager.Intstance.CreateModule("ModuleA");
            ModuleManager.Intstance.CreateModule("ModuleB");

            GlobalEvent.OnLogin.Invoke(true);
        }
    }
}

