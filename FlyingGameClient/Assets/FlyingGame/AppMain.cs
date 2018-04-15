using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SGF;

using Kurisu.Service.Core;
using Kurisu.Service.UIManager;
using Kurisu.Module;
using Kurisu.UI;
using Kurisu.Game;

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

            // UIManager.Instance.EnterMainPage();
        }

        private void InitServiceModules()
        {
            ModuleManager.Instance.Init();

            UIManager.Instance.Init("ui/");
            UIManager.MainPage = UIDef.UIHomePage;

            GameLogicManager.Instance.Init();
        }

        private void InitBusinessModules()
        {
            ModuleManager.Instance.CreateModule(ModuleDef.CharacterTouchModule);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

