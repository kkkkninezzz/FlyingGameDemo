using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kurisu.Service.Core;
using Kurisu.UI;

namespace Kurisu.Module.Home
{
    public class HomeModule : BusinessModule
    {
        /// <summary>
        /// 打开模块
        /// </summary>
        /// <param name="name"></param>
        public void OpenModule(string name, object arg = null)
        {
            switch (name)
            {
                case ModuleDef.PveModule:
                case ModuleDef.SettingModule:
                case ModuleDef.HelpModule:
                    ModuleManager.Instance.ShowModule(name, arg);
                    break;
                default:
                    UIAPI.ShowMsgBox(name, "模块正在开发中...", "确定");
                    break;
            }
            
        }
    }
}


