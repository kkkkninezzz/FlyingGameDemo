using System.Collections;

namespace Kurisu.Service.Core
{
    public static class GlobalEvent
    {
        // 定义全局事件
        public static ModuleEvent<bool> OnLogin = new ModuleEvent<bool>();
    }
}

