using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SGF;

using Kurisu.Service.Core;
using Kurisu.Service.UIManager;
using Kurisu.Module;
using Kurisu.UI;
using Kurisu.Game;
using Kurisu.Module.Map;

namespace Kurisu
{
    public class AppMain : MonoBehaviour
    {

        // Use this for initialization

        void Start()
        {
            Debugger.EnableLog = true;

            AppConfig.Init();

            InitServiceModules();
            InitBusinessModules();

            // 进入主界面
            UIManager.Instance.EnterMainPage();
        }

        private void InitServiceModules()
        {
            ModuleManager.Instance.Init();

            UIManager.Instance.Init("ui/");
            UIManager.MainPage = UIDef.UIHomePage;

            GameLogicManager.Instance.Init();

            MapModule.Instance.Init();
        }

        private void InitBusinessModules()
        {
            ModuleManager.Instance.CreateModule(ModuleDef.HomeModule);
            ModuleManager.Instance.CreateModule(ModuleDef.CharacterTouchModule);
            ModuleManager.Instance.CreateModule(ModuleDef.PveModule);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

