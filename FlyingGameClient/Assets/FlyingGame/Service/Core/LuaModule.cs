using UnityEngine;
using System.Collections;

namespace Kurisu.Service.Core
{
    public class LuaModule : BusinessModule
    {
        private object m_args = null;

        internal LuaModule(string name) : base(name)
        {

        }

        public override void Create(object args = null)
        {
            base.Create(args);
            m_args = args;

            // TODO 加载Nmae所对应的Lua脚本
        }

        public override void Release()
        {
            base.Release();

            // TODO 释放Name所对应的Lua脚本

            m_args = null;
        }
    }
}

