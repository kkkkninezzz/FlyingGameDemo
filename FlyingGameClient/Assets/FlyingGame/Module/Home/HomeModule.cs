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
        public void OpenModule(string name)
        {
            UIAPI.ShowMsgBox(name, "模块正在开发中...", "确定");
        }
    }
}


