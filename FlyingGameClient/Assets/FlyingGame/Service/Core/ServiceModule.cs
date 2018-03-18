using System;
using System.Collections;
using System.Reflection;

namespace Kurisu.Service.Core
{
    public abstract class ServiceModule<T> : Module where T : ServiceModule<T>
    {
        protected static T ms_instance = null;

        public static T Intstance
        {
            get
            {
                if (ms_instance == null)
                {
                    ms_instance = createInstance();
                }

                return ms_instance;
            }
        }

        protected static T createInstance()
        {
            // 先获取所有非public的构造方法
            ConstructorInfo[] cis = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            // 从ctors中获取无参的构造方法
            ConstructorInfo ci = Array.Find(cis, c => c.GetParameters().Length == 0);
            if (ci == null)
                throw new Exception("Non-public ctor() not found!");
            // 调用构造方法
            return ci.Invoke(null) as T;
        }
    }
}

