using UnityEngine;
using System.Collections;
using Kurisu.Service.Core;
using Kurisu.UI;

namespace Kurisu.Module.Help
{
    public class HelpModule : BusinessModule
    {

        //显示模块的主UI
        protected override void Show(object arg)
        {
            base.Show(arg);

            UIAPI.ShowUIPage(UIDef.UIHelpPage);
        }
    }
}

